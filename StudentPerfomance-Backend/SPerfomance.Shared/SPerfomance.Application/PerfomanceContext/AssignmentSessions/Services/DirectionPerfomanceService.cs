using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;

public class DirectionTypePerfomanceService
{
    private readonly List<AssignmentSessionCoursePerfomanceDTO> _coursePerfomances;

    private readonly List<AssignmentSessionDirectionTypePerfomanceDTO> _directionTypePerfomances =
    [];

    public DirectionTypePerfomanceService(
        List<AssignmentSessionCoursePerfomanceDTO> coursePerfomances
    )
    {
        _coursePerfomances = coursePerfomances;
        CalculatePerfomanceForBachelor();
        CalculatePerfomanceForMagister();
    }

    public List<AssignmentSessionDirectionTypePerfomanceDTO> DirectionTypePerfomances =>
        _directionTypePerfomances;

    private void CalculatePerfomanceForBachelor()
    {
        IEnumerable<AssignmentSessionCoursePerfomanceDTO> perfomances = _coursePerfomances.Where(
            c => c.DirectionType == "Бакалавриат"
        );

        int totals = perfomances.Count();
        if (totals == 0)
            return;

        double averageMarks = 0;
        double averagePerfomances = 0;
        foreach (AssignmentSessionCoursePerfomanceDTO perfomance in perfomances)
        {
            averageMarks += perfomance.Average;
            averagePerfomances += perfomance.Perfomance;
        }

        AssignmentSessionDirectionTypePerfomanceDTO directionPerfomance =
            new AssignmentSessionDirectionTypePerfomanceDTO()
            {
                DirectionType = "Бакалавриат",
                Average = Math.Round((double)averageMarks / totals, 3),
                Perfomance = Math.Round((double)averagePerfomances / totals, 3),
            };

        _directionTypePerfomances.Add(directionPerfomance);
    }

    private void CalculatePerfomanceForMagister()
    {
        IEnumerable<AssignmentSessionCoursePerfomanceDTO> perfomances = _coursePerfomances.Where(
            c => c.DirectionType == "Магистратура"
        );

        int totals = perfomances.Count();
        if (totals == 0)
            return;

        double averageMarks = 0;
        double averagePerfomances = 0;
        foreach (AssignmentSessionCoursePerfomanceDTO perfomance in perfomances)
        {
            averageMarks += perfomance.Average;
            averagePerfomances += perfomance.Perfomance;
        }

        AssignmentSessionDirectionTypePerfomanceDTO directionPerfomance =
            new AssignmentSessionDirectionTypePerfomanceDTO()
            {
                DirectionType = "Магистратура",
                Average = Math.Round((double)averageMarks / totals, 3),
                Perfomance = Math.Round((double)averagePerfomances / totals, 3),
            };

        _directionTypePerfomances.Add(directionPerfomance);
    }
}
