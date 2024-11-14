using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports.Parts;

public sealed class StudentStatisticsPart : DomainEntity
{
    private List<StudentStatisticsOnDiscipline> _studentStatisticsOnDisciplines = [];

    internal StudentStatisticsPart(
        Guid id,
        GroupStatisticsReport root,
        Student student,
        AssignmentWeek week
    )
        : base(id)
    {
        Group = root;
        Name = student.Name;
        Recordbook = student.Recordbook;
        FillStudentStatisticsOnDiscipline(week);
        var averageTask = CalculateAverage();
        var perfomanceTask = CalculatePerfomance();
        Task.WhenAll(averageTask, perfomanceTask).Wait();
        Average = averageTask.Result;
        Perfomance = perfomanceTask.Result;
    }

    private void FillStudentStatisticsOnDiscipline(AssignmentWeek week)
    {
        foreach (var assignment in week.Assignments)
        {
            if (HasDisciplineStatDublicate(assignment))
                continue;

            StudentAssignment? studentAssignment = GetStudentAssignment(assignment);
            if (studentAssignment == null)
                continue;

            StudentStatisticsOnDiscipline part = new StudentStatisticsOnDiscipline(
                Guid.NewGuid(),
                this,
                assignment,
                studentAssignment
            );
            _studentStatisticsOnDisciplines.Add(part);
        }
    }

    private async Task<double> CalculateAverage()
    {
        int totals = 0;
        int marksSum = 0;
        for (int index = 0; index < _studentStatisticsOnDisciplines.Count; index++)
        {
            if (_studentStatisticsOnDisciplines[index].Mark == 0)
                continue;

            totals += 1;
            marksSum += _studentStatisticsOnDisciplines[index].Mark;
        }
        if (totals == 0)
            return await Task.FromResult(0.00);
        return await Task.FromResult(Math.Round((double)marksSum / totals, 3));
    }

    private async Task<double> CalculatePerfomance()
    {
        int totals = 0;
        int positiveMarksCount = 0;
        for (int index = 0; index < _studentStatisticsOnDisciplines.Count; index++)
        {
            if (_studentStatisticsOnDisciplines[index].Mark == 0)
                continue;

            if (_studentStatisticsOnDisciplines[index].Mark == 2)
            {
                totals += 1;
                continue;
            }

            totals += 1;
            positiveMarksCount += 1;
            continue;
        }

        double percentage = (double)positiveMarksCount / totals * 100;
        return await Task.FromResult(percentage);
    }

    private bool HasDisciplineStatDublicate(Assignment assignment) =>
        _studentStatisticsOnDisciplines.Any(stat =>
            stat.DisciplineName == assignment.Discipline.Discipline
        );

    private StudentAssignment? GetStudentAssignment(Assignment assignment) =>
        assignment.StudentAssignments.FirstOrDefault(a => a.Student.Recordbook == Recordbook);

    public IReadOnlyCollection<StudentStatisticsOnDiscipline> StudentStatisticsOnDisciplines =>
        _studentStatisticsOnDisciplines;

    public GroupStatisticsReport Group { get; init; }

    public StudentName Name { get; init; }

    public StudentRecordbook Recordbook { get; init; }

    public double Perfomance { get; }

    public double Average { get; }
}
