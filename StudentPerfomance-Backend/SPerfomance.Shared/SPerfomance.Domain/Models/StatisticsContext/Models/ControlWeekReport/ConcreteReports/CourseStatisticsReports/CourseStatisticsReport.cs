using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.Parts;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports;

public class CourseStatisticsReport : DomainEntity
{
    private List<CoursePart> _parts = [];

    public ControlWeekReport Root { get; init; }

    public IReadOnlyCollection<CoursePart> Parts => _parts;

    public double Perfomance { get; init; }

    public double Average { get; init; }

    private CourseStatisticsReport(Guid id)
        : base(id)
    {
        Root = null!;
    }

    internal CourseStatisticsReport(Guid id, ControlWeekReport root, AssignmentSession session)
        : this(id)
    {
        Root = root;
        FillWithCourseParts(session);
        var perfomanceTask = CalculatePerfomance();
        var averageTask = CalculateAverage();
        Perfomance = perfomanceTask.Result;
        Average = averageTask.Result;
    }

    private void FillWithCourseParts(AssignmentSession session)
    {
        foreach (var week in session.Weeks)
        {
            CoursePart part = new CoursePart(Guid.NewGuid(), this, week);
            _parts.Add(part);
        }
    }

    private async Task<double> CalculatePerfomance()
    {
        int totals = _parts.Count;
        if (totals == 0)
            return await Task.FromResult(0.00);

        double averagePerfomance = _parts.Sum(p => p.Perfomance);
        return averagePerfomance / totals;
    }

    private async Task<double> CalculateAverage()
    {
        int totals = _parts.Count();
        if (totals == 0)
            return await Task.FromResult(0.00);

        double averageSum = _parts.Sum(p => p.Average);
        return Math.Round(averageSum / totals, 2);
    }
}
