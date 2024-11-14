using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public class DirectionCodePerfomanceService
{
    private readonly List<AssignmentWeekDTO> _weeks;

    public DirectionCodePerfomanceService(List<AssignmentWeekDTO> weeks)
    {
        _weeks = weeks;
    }

    public async Task<List<AssignmentSessionDirectionCodePerfomanceDTO>> Calculate()
    {
        List<AssignmentSessionDirectionCodePerfomanceDTO> perfomances = [];
        List<string> codes = [];
        foreach (var week in _weeks)
        {
            if (codes.Contains(week.DirectionCode))
                continue;

            codes.Add(week.DirectionCode);
        }

        foreach (var code in codes)
        {
            AssignmentSessionDirectionCodePerfomanceDTO perfomance =
                new AssignmentSessionDirectionCodePerfomanceDTO();
            perfomance.DirectionCode = code;
            int totals = 0;
            double averageMarksSum = 0;
            double averagePerfomanceSum = 0;
            IEnumerable<AssignmentWeekDTO> codeWeeks = _weeks.Where(w => w.DirectionCode == code);
            totals = codeWeeks.Count();

            if (totals == 0)
                return await Task.FromResult(perfomances);

            foreach (var week in codeWeeks)
            {
                averageMarksSum += week.AverageMarks;
                averagePerfomanceSum += week.AveragePerfomancePercent;
            }

            perfomance.Average = Math.Round((double)averageMarksSum / totals, 3);
            perfomance.Perfomance = Math.Round((double)averagePerfomanceSum / totals, 3);
            perfomances.Add(perfomance);
        }

        return await Task.FromResult(perfomances);
    }
}
