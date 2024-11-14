using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionCodeReports.Parts;

public sealed class DirectionCodeReportPart : DomainEntity
{
    public DirectionCodeReport Root { get; init; }

    public DirectionCode Code { get; init; }

    public double Average { get; init; }

    public double Perfomance { get; init; }

    internal DirectionCodeReportPart(
        Guid id,
        DirectionCodeReport root,
        DirectionCode code,
        IEnumerable<AssignmentWeek> weeks
    )
        : base(id)
    {
        Root = root;
        Code = code;
        var averageTask = CalculateAverage(weeks);
        var perfomanceTask = CalculatePerfomance(weeks);
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private async Task<double> CalculateAverage(IEnumerable<AssignmentWeek> weeks)
    {
        int totals = weeks.Count();
        if (totals == 0)
            return await Task.FromResult(0.00);

        double sumAverage = 0;
        List<Task<double>> calculationTasks = [];
        foreach (var week in weeks)
        {
            calculationTasks.Add(week.CalculateAverage());
        }

        await Task.WhenAll(calculationTasks);
        sumAverage = calculationTasks.Select(t => t.Result).Sum();
        return Math.Round(sumAverage / totals, 3);
    }

    private async Task<double> CalculatePerfomance(IEnumerable<AssignmentWeek> weeks)
    {
        int totals = weeks.Count();
        if (totals == 0)
            return await Task.FromResult(0.00);

        List<Task<double>> calculationTasks = [];
        foreach (var week in weeks)
        {
            calculationTasks.Add(week.CalculatePerfomance());
        }

        await Task.WhenAll(calculationTasks);
        double sumAverage = calculationTasks.Select(t => t.Result).Sum();
        return sumAverage / totals;
    }
}
