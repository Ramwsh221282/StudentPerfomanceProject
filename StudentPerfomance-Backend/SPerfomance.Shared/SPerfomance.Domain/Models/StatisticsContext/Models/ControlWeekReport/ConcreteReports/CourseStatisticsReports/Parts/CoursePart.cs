using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.CourseStatisticsReports.Parts;

public sealed class CoursePart : DomainEntity
{
    public CourseStatisticsReport Root { get; init; }

    public Course Course { get; init; }

    public double Perfomance { get; init; }

    public double Average { get; init; }

    private CoursePart(Guid id)
        : base(id)
    {
        Root = null!;
        Course = null!;
    }

    internal CoursePart(Guid id, CourseStatisticsReport root, AssignmentWeek week)
        : base(id)
    {
        Root = root;
        Course = new Course(week.Group.ActiveGroupSemester!);
        var perfomanceTask = CalculatePerfomance(week);
        var averageTask = CalculateAverage(week);
        Task.WhenAll(perfomanceTask, averageTask).Wait();
        Perfomance = perfomanceTask.Result;
        Average = averageTask.Result;
    }

    private async Task<double> CalculatePerfomance(AssignmentWeek week)
    {
        int totals = 0;
        int positiveMarks = 0;
        foreach (var assignment in week.Assignments)
        {
            foreach (var studentAssignment in assignment.StudentAssignments)
            {
                if (studentAssignment.IsEmpty())
                    continue;

                if (studentAssignment.IsBadMark() || studentAssignment.IsNotAttestated())
                {
                    totals += 1;
                    continue;
                }

                totals += 1;
                positiveMarks += 1;
            }
        }

        if (totals == 0)
            return await Task.FromResult(0.00);

        double percentage = (double)positiveMarks / totals * 100;
        return await Task.FromResult(percentage);
    }

    private async Task<double> CalculateAverage(AssignmentWeek week)
    {
        int totals = 0;
        int marksSum = 0;
        foreach (var assignment in week.Assignments)
        {
            foreach (var studentAssignment in assignment.StudentAssignments)
            {
                if (studentAssignment.IsEmpty())
                    continue;

                if (studentAssignment.IsBadMark() || studentAssignment.IsNotAttestated())
                {
                    totals += 1;
                    marksSum += 2;
                    continue;
                }

                totals += 1;
                marksSum += studentAssignment.Value;
            }
        }

        if (totals == 0)
            return await Task.FromResult(0.00);

        return Math.Round((double)marksSum / totals, 3);
    }
}
