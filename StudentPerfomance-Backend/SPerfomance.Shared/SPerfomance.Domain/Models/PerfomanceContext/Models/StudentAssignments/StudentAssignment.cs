using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

public class StudentAssignment : DomainEntity
{
    public Assignment Assignment { get; init; }

    public AssignmentValue Value { get; private set; }

    public Student Student { get; init; }

    internal StudentAssignment()
        : base(Guid.Empty)
    {
        Assignment = new Assignment();
        Value = AssignmentValue.Empty;
        Student = Student.Empty;
    }

    internal StudentAssignment(Assignment assignment, Student student)
        : base(Guid.NewGuid())
    {
        Assignment = assignment;
        Value = AssignmentValue.Empty;
        Student = student;
    }

    public Result<StudentAssignment> Assign(int value)
    {
        Result<AssignmentValue> assignment = AssignmentValue.Create((byte)value);
        if (assignment.IsFailure)
            return assignment.Error;

        Value = assignment.Value;
        return this;
    }
}

public static class StudentAssignmentExtensions
{
    public static bool IsEmpty(this StudentAssignment assignment) =>
        assignment.Value == AssignmentValue.Empty;

    public static bool IsBadMark(this StudentAssignment assignment) =>
        assignment.Value == AssignmentValue.Bad;

    public static bool IsNotAttestated(this StudentAssignment assignment) =>
        assignment.Value == AssignmentValue.NotAttestated;

    public static double CalculateAverage(this IEnumerable<StudentAssignment> assignments)
    {
        int totals = 0;
        int marksSum = 0;

        foreach (var assignment in assignments)
        {
            if (assignment.IsEmpty())
                continue;

            if (assignment.IsNotAttestated())
            {
                totals += 1;
                marksSum += 2;
            }

            totals += 1;
            marksSum += assignment.Value;
        }

        return totals == 0 ? 0.00 : Math.Round((double)marksSum / totals, 3);
    }

    public static double CalculatePerfomance(this IEnumerable<StudentAssignment> assignments)
    {
        int totals = 0;
        int positiveMarksCount = 0;

        foreach (var assignment in assignments)
        {
            if (assignment.IsEmpty())
                continue;

            if (assignment.IsBadMark() || assignment.IsNotAttestated())
            {
                totals += 1;
                continue;
            }

            totals += 1;
            positiveMarksCount += 1;
        }

        return totals == 0 ? 0.00 : (double)positiveMarksCount / totals * 100;
    }
}
