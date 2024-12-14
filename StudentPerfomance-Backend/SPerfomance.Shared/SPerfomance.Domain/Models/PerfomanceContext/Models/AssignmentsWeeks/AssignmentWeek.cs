using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;

public class AssignmentWeek : DomainEntity
{
    private readonly List<Assignment> _assignments = [];

    public AssignmentSession.AssignmentSession Session { get; private init; }

    public StudentGroup Group { get; private init; }

    internal AssignmentWeek()
        : base(Guid.Empty)
    {
        Group = StudentGroup.Empty;
        Session = default!;
    }

    internal AssignmentWeek(StudentGroup group, AssignmentSession.AssignmentSession session)
        : base(Guid.NewGuid())
    {
        Group = group;
        Session = session;
        FillEmptyAssignments(group);
    }

    public IReadOnlyCollection<Assignment> Assignments => _assignments;

    internal static AssignmentWeek Empty => new AssignmentWeek();

    private void FillEmptyAssignments(StudentGroup group)
    {
        var disciplines = group.ActiveGroupSemester!.Disciplines;

        foreach (var discipline in disciplines)
        {
            var assignment = new Assignment(discipline, this, group);
            _assignments.Add(assignment);
        }
    }
}

internal static class AssignmentWeekExtensions
{
    internal static async Task<double> CalculatePerfomance(this AssignmentWeek week)
    {
        var totals = week.Assignments.Count;
        if (totals == 0)
            return 0.00;

        double sumPerfomances;
        List<Task<double>> calculationTasks = [];
        foreach (var assignment in week.Assignments)
        {
            calculationTasks.Add(assignment.CalculatePerfomance());
        }
        await Task.WhenAll(calculationTasks);
        sumPerfomances = calculationTasks.Select(t => t.Result).Sum();
        return sumPerfomances / totals;
    }

    internal static async Task<double> CalculateAverage(this AssignmentWeek week)
    {
        var totals = week.Assignments.Count;
        if (totals == 0)
            return 0.00;

        double sumAverages;
        List<Task<double>> calculationTasks = [];
        foreach (var assignment in week.Assignments)
        {
            calculationTasks.Add(assignment.CalculateAverage());
        }
        await Task.WhenAll(calculationTasks);
        sumAverages = calculationTasks.Select(t => t.Result).Sum();
        return Math.Round(sumAverages / totals, 3);
    }
}
