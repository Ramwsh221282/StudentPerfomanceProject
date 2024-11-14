using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class StudentAssignmentDTO
{
    public Guid Id { get; init; }

    public string Value { get; init; }

    public string StudentName { get; init; }

    public string StudentSurname { get; init; }

    public string? StudentPatronymic { get; init; }

    public ulong StudentRecordbook { get; init; }

    public double StudentAverage { get; set; }

    public double StudentPerfomance { get; set; }

    public StudentAssignmentDTO(StudentAssignment studentAssignment)
    {
        Id = studentAssignment.Id;
        Value = MapAssignmentValue(studentAssignment);
        StudentName = studentAssignment.Student.Name.Name;
        StudentSurname = studentAssignment.Student.Name.Surname;
        StudentPatronymic = studentAssignment.Student.Name.Patronymic;
        StudentRecordbook = studentAssignment.Student.Recordbook.Recordbook;
    }

    private string MapAssignmentValue(StudentAssignment studentAssignment)
    {
        return studentAssignment.Value.Value switch
        {
            0 => "Неаттестация",
            1 => "Нет проставления",
            2 => "2",
            3 => "3",
            4 => "4",
            5 => "5",
            _ => "Нет проставления",
        };
    }
}
