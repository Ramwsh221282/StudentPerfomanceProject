using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class StudentMarkAssignmentFromTeacherDTO
{
    public Guid Id { get; init; }

    public byte Value { get; init; }

    public string StudentName { get; init; }

    public string StudentSurname { get; init; }

    public string? StudentPatronymic { get; init; }

    public ulong StudentRecordbook { get; init; }

    public StudentMarkAssignmentFromTeacherDTO(StudentAssignment studentAssignment)
    {
        Id = studentAssignment.Id;
        Value = studentAssignment.Value.Value;
        StudentName = studentAssignment.Student.Name.Name;
        StudentSurname = studentAssignment.Student.Name.Surname;
        StudentPatronymic = studentAssignment.Student.Name.Patronymic;
        StudentRecordbook = studentAssignment.Student.Recordbook.Recordbook;
    }
}
