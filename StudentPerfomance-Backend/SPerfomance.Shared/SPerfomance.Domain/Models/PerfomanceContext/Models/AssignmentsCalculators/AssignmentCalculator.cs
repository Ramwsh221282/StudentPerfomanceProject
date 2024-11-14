using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsCalculators;

internal sealed class AssignmentCalculator
{
    internal double CalculatePerfomance(AssignmentValue[] values)
    {
        var totals = GetTotals(values);
        if (totals == 0)
            return 0.00;

        var positiveMarks = values.Count(v =>
            v != AssignmentValue.Bad && v != AssignmentValue.NotAttestated
        );

        return Math.Round((double)positiveMarks / totals * 100, 3);
    }

    internal double CalculateAverage(AssignmentValue[] values)
    {
        var totals = GetTotals(values);
        if (totals == 0)
            return 0.00;

        var marksSum = values
            .Where(t => t != AssignmentValue.Empty)
            .Aggregate(0, (current, t) => current + t);

        return Math.Round((double)marksSum / totals, 3);
    }

    private int GetTotals(AssignmentValue[] values) =>
        values.Count(v => v != AssignmentValue.Empty);
}
