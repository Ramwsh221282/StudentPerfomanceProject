using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.TeacherDepartments.ValueObjects;
using SPerfomance.Domain.Models.Teachers.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.DepartmentStatisticsReports.Parts;

public sealed class DepartmentStatisticsReportPart : DomainEntity
{
    private readonly List<TeacherStatisticsReportPart> _parts = [];

    public DepartmentStatisticsReport Root { get; init; }

    public DepartmentName DepartmentName { get; init; }

    public double Average { get; init; }

    public double Perfomance { get; init; }

    public IReadOnlyCollection<TeacherStatisticsReportPart> Parts => _parts;

    internal DepartmentStatisticsReportPart(
        Guid id,
        DepartmentStatisticsReport root,
        DepartmentName name,
        IEnumerable<AssignmentWeek> weeks
    )
        : base(id)
    {
        Root = root;
        DepartmentName = name;
        IEnumerable<StudentAssignment> departmentDisciplines = GetDepartmentDisciplines(weeks);
        var averageTask = CalculateAverage(departmentDisciplines);
        var perfomanceTask = CalculatePerfomance(departmentDisciplines);
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
        FillWithTeacherParts(departmentDisciplines).Wait();
    }

    private StudentAssignment[] GetDepartmentDisciplines(IEnumerable<AssignmentWeek> weeks) =>
        weeks
            .SelectMany(w => w.Assignments)
            .SelectMany(a => a.StudentAssignments)
            .Where(sa => sa.Assignment.Discipline.Teacher!.Department.Name == DepartmentName)
            .ToArray();

    private async Task<double> CalculateAverage(IEnumerable<StudentAssignment> assignments) =>
        await Task.FromResult(Math.Round(assignments.CalculateAverage(), 3));

    private async Task<double> CalculatePerfomance(IEnumerable<StudentAssignment> assignments) =>
        await Task.FromResult(assignments.CalculatePerfomance());

    private async Task FillWithTeacherParts(IEnumerable<StudentAssignment> assignments)
    {
        HashSet<TeacherName> teacherNames = assignments
            .Select(a => a.Assignment.Discipline.Teacher!.Name)
            .Distinct()
            .ToHashSet();

        foreach (var teacher in teacherNames)
        {
            IEnumerable<StudentAssignment> assignmentsOfTeacher = assignments.Where(a =>
                a.Assignment.Discipline.Teacher!.Name == teacher
            );

            TeacherStatisticsReportPart part = new TeacherStatisticsReportPart(
                Guid.NewGuid(),
                this,
                teacher,
                DepartmentName,
                assignmentsOfTeacher
            );

            _parts.Add(part);
        }

        await Task.CompletedTask;
    }
}
