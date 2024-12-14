using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionTypeReports.Parts;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionTypeReports;

public sealed class DirectionTypeReport : DomainEntity
{
    private readonly List<DirectionTypeReportPart> _parts = [];

    public IReadOnlyCollection<DirectionTypeReportPart> Parts => _parts;

    public ControlWeekReport Root { get; init; }

    public double Perfomance { get; init; }

    public double Average { get; init; }

    internal DirectionTypeReport(Guid id, ControlWeekReport root, AssignmentSession session)
        : base(id)
    {
        Root = root;
        Task.WhenAll(
                InitializePartByDirectionType(session, DirectionType.Bachelor),
                InitializePartByDirectionType(session, DirectionType.Magister)
            )
            .Wait();
        var averageTask = CalculateAverage();
        var perfomanceTask = CalculatePerfomance();
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private async Task InitializePartByDirectionType(AssignmentSession session, DirectionType type)
    {
        IEnumerable<AssignmentWeek> weeks = session.Weeks.Where(w =>
            w.Group.EducationPlan!.Direction.Type == type
        );

        if (weeks.Count() == 0)
            return;

        DirectionTypeReportPart part = new DirectionTypeReportPart(
            Guid.NewGuid(),
            this,
            type,
            weeks
        );
        _parts.Add(part);
        await Task.CompletedTask;
    }

    private async Task<double> CalculatePerfomance()
    {
        int totals = _parts.Count;
        if (totals == 0)
            await Task.FromResult(0.00);

        double sumPerfomance = _parts.Sum(p => p.Perfomance);
        return Math.Round(sumPerfomance / totals, 3);
    }

    private async Task<double> CalculateAverage()
    {
        int totals = _parts.Count;
        if (totals == 0)
            return await Task.FromResult(0.00);

        double sumAverage = _parts.Sum(p => p.Average);
        return sumAverage / totals;
    }
}
