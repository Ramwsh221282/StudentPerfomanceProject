using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;
using SPerfomance.Domain.Models.Teachers.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports.Parts;

public sealed class TeacherStatisticsReportPart : DomainEntity
{
    public DepartmentStatisticsReportPart Root { get; init; }

    public TeacherName TeacherName { get; init; }

    public DepartmentName DepartmentName { get; init; }

    public double Average { get; init; }

    public double Perfomance { get; init; }

    internal TeacherStatisticsReportPart(
        Guid id,
        DepartmentStatisticsReportPart root,
        TeacherName teacherName,
        DepartmentName departmentName,
        IEnumerable<StudentAssignment> assignments
    )
        : base(id)
    {
        Root = root;
        TeacherName = teacherName;
        DepartmentName = departmentName;
        var averageTask = CalculateAverage(assignments);
        var perfomanceTask = CalculatePerfomance(assignments);
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private async Task<double> CalculateAverage(
        IEnumerable<StudentAssignment> studentAssignments
    ) => await Task.FromResult(Math.Round(studentAssignments.CalculateAverage(), 3));

    private async Task<double> CalculatePerfomance(
        IEnumerable<StudentAssignment> studentAssignments
    ) => await Task.FromResult(studentAssignments.CalculatePerfomance());
}
