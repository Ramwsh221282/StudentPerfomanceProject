using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports.Parts;
using SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports;

public sealed class DepartmentStatisticsReport : DomainEntity
{
    private readonly List<DepartmentStatisticsReportPart> _parts = [];

    public IReadOnlyCollection<DepartmentStatisticsReportPart> Parts => _parts;

    public ControlWeekReport Root { get; init; }

    public double Perfomance { get; init; }

    public double Average { get; init; }

    internal DepartmentStatisticsReport(Guid id, ControlWeekReport root, AssignmentSession session)
        : base(id)
    {
        Root = root;
        FillWithDepartmentStatisticsParts(session);
        var averageTask = CalculateAverage();
        var perfomanceTask = CalculatePerfomance();
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private void FillWithDepartmentStatisticsParts(AssignmentSession session)
    {
        HashSet<DepartmentName> names = session
            .Weeks.SelectMany(w => w.Assignments)
            .Select(a => a.Discipline.Teacher!.Department.Name)
            .Distinct()
            .ToHashSet();

        foreach (var name in names)
        {
            DepartmentStatisticsReportPart part = new DepartmentStatisticsReportPart(
                Guid.NewGuid(),
                this,
                name,
                session.Weeks
            );

            _parts.Add(part);
        }
    }

    private async Task<double> CalculateAverage()
    {
        int totals = _parts.Count;
        return totals == 0
            ? 0.00
            : await Task.FromResult(Math.Round(_parts.Select(p => p.Average).Sum() / totals, 3));
    }

    private async Task<double> CalculatePerfomance()
    {
        int totals = _parts.Count;
        return totals == 0
            ? 0.00
            : await Task.FromResult(_parts.Select(p => p.Perfomance).Sum() / totals);
    }
}
