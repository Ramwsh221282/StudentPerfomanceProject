using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DirectionTypeReports.Parts;

public sealed class DirectionTypeReportPart : DomainEntity
{
    public DirectionTypeReport Root { get; init; }

    public DirectionType Type { get; init; }

    public double Average { get; init; }

    public double Perfomance { get; init; }

    internal DirectionTypeReportPart(
        Guid id,
        DirectionTypeReport root,
        DirectionType type,
        IEnumerable<AssignmentWeek> weeks
    )
        : base(id)
    {
        Root = root;
        Type = type;
        var perfomanceTask = CalculatePerfomance(weeks);
        var averageTask = CalculateAverage(weeks);
        Task.WhenAll(perfomanceTask, averageTask).Wait();
        Perfomance = perfomanceTask.Result;
        Average = averageTask.Result;
    }

    private async Task<double> CalculateAverage(IEnumerable<AssignmentWeek> weeks)
    {
        int count = weeks.Count();
        if (count == 0)
            return 0.00;

        double averageSum = 0;
        List<Task<double>> calculationTasks = [];
        foreach (AssignmentWeek week in weeks)
        {
            calculationTasks.Add(week.CalculateAverage());
        }
        await Task.WhenAll(calculationTasks);
        averageSum = calculationTasks.Select(t => t.Result).Sum();
        return Math.Round(averageSum / count, 3);
    }

    private async Task<double> CalculatePerfomance(IEnumerable<AssignmentWeek> weeks)
    {
        int count = weeks.Count();
        if (count == 0)
            return 0.00;

        double averagePerfomance = 0;
        List<Task<double>> calculationTasks = [];
        foreach (AssignmentWeek week in weeks)
        {
            calculationTasks.Add(week.CalculatePerfomance());
        }
        await Task.WhenAll(calculationTasks);
        averagePerfomance = calculationTasks.Select(t => t.Result).Sum();
        return averagePerfomance / count;
    }
}
