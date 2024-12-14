using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.ValueObjects;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

public class Assignment : DomainEntity
{
    public AssignmentWeek Week { get; init; }

    public SemesterPlan Discipline { get; init; }

    private List<StudentAssignment> _studentAssignments = [];

    public IReadOnlyCollection<StudentAssignment> StudentAssignments => _studentAssignments;

    internal Assignment()
        : base(Guid.Empty)
    {
        Discipline = SemesterPlan.Empty;
        Week = new AssignmentWeek();
    }

    internal Assignment(SemesterPlan discipline, AssignmentWeek week, StudentGroup group)
        : base(Guid.NewGuid())
    {
        Discipline = discipline;
        Week = week;
        FillEmptyStudentAssignments(group);
    }

    internal void FillEmptyStudentAssignments(StudentGroup group)
    {
        foreach (Student student in group.Students)
        {
            if (student.State == StudentState.NotActive)
                continue;

            StudentAssignment studentAssignment = new StudentAssignment(this, student);
            _studentAssignments.Add(studentAssignment);
        }
    }
}

internal static class AssignmentWeekExtensions
{
    internal static async Task<double> CalculateAverage(this Assignment assignment)
    {
        int totals = 0;
        int marksSum = 0;

        List<Task> calculationTasks = [];
        foreach (var studentAssignment in assignment.StudentAssignments)
        {
            calculationTasks.Add(
                Task.Run(async () =>
                {
                    if (studentAssignment.IsEmpty())
                        await Task.CompletedTask;

                    if (studentAssignment.IsNotAttestated() || studentAssignment.IsBadMark())
                    {
                        totals += 1;
                        marksSum += 2;
                        await Task.CompletedTask;
                    }

                    totals += 1;
                    marksSum += studentAssignment.Value;
                })
            );
        }
        await Task.WhenAll(calculationTasks);
        if (totals == 0)
            return await Task.FromResult(0.00);

        return await Task.FromResult(Math.Round((double)marksSum / totals, 3));
    }

    internal static async Task<double> CalculatePerfomance(this Assignment assignment)
    {
        int totals = 0;
        int positiveMarks = 0;

        List<Task> calculationTasks = [];
        foreach (var studentAssignment in assignment.StudentAssignments)
        {
            calculationTasks.Add(
                Task.Run(async () =>
                {
                    if (studentAssignment.IsEmpty())
                        await Task.CompletedTask;

                    if (studentAssignment.IsNotAttestated() || studentAssignment.IsBadMark())
                    {
                        totals += 1;
                        await Task.CompletedTask;
                    }

                    totals += 1;
                    positiveMarks += 1;
                })
            );
        }
        await Task.WhenAll(calculationTasks);
        if (totals == 0)
            return await Task.FromResult(0.00);

        double percentage = positiveMarks / totals * 100;
        return await Task.FromResult(percentage);
    }
}
