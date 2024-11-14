using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.SemesterPlans.ValueObjects;

namespace SPerfomance.Domain.Models.StatisticsContext.Models.ControlWeekReport.ConcreteReports.GroupStatisticsReports.Parts;

public sealed class StudentStatisticsOnDiscipline : DomainEntity
{
    public StudentStatisticsPart StudentPart { get; init; }

    public DisciplineName DisciplineName { get; init; }

    public byte Mark { get; init; }

    internal StudentStatisticsOnDiscipline(
        Guid id,
        StudentStatisticsPart studentPart,
        Assignment assignment,
        StudentAssignment studentAssignment
    )
        : base(id)
    {
        StudentPart = studentPart;
        DisciplineName = assignment.Discipline.Discipline;
        Mark = GetMarkValueFromAssignment(studentAssignment);
    }

    private byte GetMarkValueFromAssignment(StudentAssignment studentAssignment)
    {
        return studentAssignment.Value.Value switch
        {
            0 => 2,
            1 => 0,
            2 => 2,
            3 => 3,
            4 => 4,
            5 => 5,
            _ => 0,
        };
    }
}
