using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.Semesters.ValueObjects;
using SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports.Parts;
using SPerfomance.Domain.Models.StudentGroups.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports;

public sealed class GroupStatisticsReport : DomainEntity
{
    private readonly List<StudentStatisticsPart> _studentStatistics = [];

    internal GroupStatisticsReport(Guid id, ControlWeekReport root, AssignmentWeek week)
        : base(id)
    {
        StudentGroupName = week.Group.Name;
        AtSemester = week.Group.ActiveGroupSemester!.Number;
        DirectionCode = week.Group.ActiveGroupSemester!.Plan.Direction.Code;
        DirectionType = week.Group.ActiveGroupSemester!.Plan.Direction.Type;
        Root = root;
        FillStudentStatistics(week);
        var averageTask = CalculateAverage();
        var perfomanceTask = CalculatePerfomance();
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private void FillStudentStatistics(AssignmentWeek week)
    {
        Assignment firstAssignment = week.Assignments.First();
        foreach (var studentAssignment in firstAssignment.StudentAssignments)
        {
            StudentStatisticsPart part = new StudentStatisticsPart(
                Guid.NewGuid(),
                this,
                studentAssignment.Student,
                week
            );

            if (_studentStatistics.Any(s => s.Name == part.Name && s.Recordbook == part.Recordbook))
                return;
            _studentStatistics.Add(part);
        }
    }

    private async Task<double> CalculateAverage()
    {
        int totals = _studentStatistics.Count;
        if (totals == 0)
            return await Task.FromResult(0.00);

        double sum = _studentStatistics.Sum(s => s.Average);
        return await Task.FromResult(Math.Round(sum / totals, 3));
    }

    private async Task<double> CalculatePerfomance()
    {
        int totals = _studentStatistics.Count;
        if (totals == 0)
            return await Task.FromResult(0.00);

        double sum = _studentStatistics.Sum(s => s.Perfomance);
        return await Task.FromResult(Math.Round(sum / totals, 3));
    }

    public double Perfomance { get; init; }

    public double Average { get; init; }

    public IReadOnlyCollection<StudentStatisticsPart> Parts => _studentStatistics;

    public ControlWeekReport Root { get; init; }

    public StudentGroupName StudentGroupName { get; init; }

    public SemesterNumber AtSemester { get; init; }

    public DirectionCode DirectionCode { get; init; }

    public DirectionType DirectionType { get; init; }
}
