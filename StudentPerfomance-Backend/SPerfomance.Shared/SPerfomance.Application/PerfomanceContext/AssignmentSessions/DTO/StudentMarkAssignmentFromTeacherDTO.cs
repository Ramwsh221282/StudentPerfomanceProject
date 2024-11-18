using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class StudentMarkAssignmentFromTeacherDTO(StudentAssignment studentAssignment)
{
    public Guid Id { get; init; } = studentAssignment.Id;

    public byte Value { get; init; } = studentAssignment.Value.Value;

    public string StudentName { get; init; } = studentAssignment.Student.Name.Name;

    public string StudentSurname { get; init; } = studentAssignment.Student.Name.Surname;

    public string? StudentPatronymic { get; init; } = studentAssignment.Student.Name.Patronymic;

    public ulong StudentRecordbook { get; init; } = studentAssignment.Student.Recordbook.Recordbook;
}
