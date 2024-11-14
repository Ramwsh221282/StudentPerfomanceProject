using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionCodeReports.Parts;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionCodeReports;

public sealed class DirectionCodeReport : DomainEntity
{
    private readonly List<DirectionCodeReportPart> _parts = [];

    public IReadOnlyCollection<DirectionCodeReportPart> Parts => _parts;

    public double Average { get; init; }

    public double Perfomance { get; init; }

    public ControlWeekReport Root { get; init; }

    internal DirectionCodeReport(Guid id, ControlWeekReport root, AssignmentSession session)
        : base(id)
    {
        Root = root;
        FillDirectionCodeParts(session).Wait();
        var averageTask = CalculateAverage();
        var perfomanceTask = CalculatePerfomance();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private async Task FillDirectionCodeParts(AssignmentSession session)
    {
        if (session.Weeks.Count() == 0)
            await Task.CompletedTask;

        IEnumerable<AssignmentWeek> weeks = session.Weeks;
        HashSet<DirectionCode> codes = [];

        foreach (var week in session.Weeks)
        {
            codes.Add(week.Group.EducationPlan!.Direction.Code);
        }

        List<Task> creationTasks = [];
        foreach (var code in codes)
        {
            creationTasks.Add(
                Task.Run(async () =>
                {
                    IEnumerable<AssignmentWeek> codeWeeks = session.Weeks.Where(w =>
                        w.Group.EducationPlan!.Direction.Code == code
                    );

                    if (codeWeeks.Count() == 0)
                        await Task.CompletedTask;

                    DirectionCodeReportPart part = new DirectionCodeReportPart(
                        Guid.NewGuid(),
                        this,
                        code,
                        codeWeeks
                    );

                    _parts.Add(part);
                })
            );
        }

        await Task.WhenAll(creationTasks);
    }

    private async Task<double> CalculateAverage()
    {
        int totals = _parts.Count;
        if (totals == 0)
            return await Task.FromResult(0.00);

        double sumAverage = _parts.Select(p => p.Average).Sum();
        return Math.Round(sumAverage / totals, 3);
    }

    private async Task<double> CalculatePerfomance()
    {
        int totals = _parts.Count;
        if (totals == 0)
            return await Task.FromResult(0.00);

        double sumAverage = _parts.Select(p => p.Perfomance).Sum();
        return sumAverage / totals;
    }
}
