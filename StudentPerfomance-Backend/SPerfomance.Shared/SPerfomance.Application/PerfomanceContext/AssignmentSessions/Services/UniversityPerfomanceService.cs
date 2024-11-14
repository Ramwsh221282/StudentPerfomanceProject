using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public class UniversityPerfomanceService
{
    private readonly List<AssignmentWeekDTO> _weeks;

    public UniversityPerfomanceService(List<AssignmentWeekDTO> weeks)
    {
        _weeks = weeks;
    }

    public async Task<AssignmentSessionUniversityPerfomanceDTO> CalculateForUniversity()
    {
        AssignmentSessionUniversityPerfomanceDTO perfomance =
            new AssignmentSessionUniversityPerfomanceDTO();
        int totals = _weeks.Count;

        if (totals == 0)
            return await Task.FromResult(perfomance);

        double sumMarks = 0;
        double sumPerfomance = 0;
        foreach (AssignmentWeekDTO week in _weeks)
        {
            sumMarks += week.AverageMarks;
            sumPerfomance += week.AveragePerfomancePercent;
        }

        perfomance.Average = Math.Round((double)sumMarks / totals, 3);
        perfomance.Perfomance = Math.Round((double)sumPerfomance / totals, 3);
        return await Task.FromResult(perfomance);
    }
}
