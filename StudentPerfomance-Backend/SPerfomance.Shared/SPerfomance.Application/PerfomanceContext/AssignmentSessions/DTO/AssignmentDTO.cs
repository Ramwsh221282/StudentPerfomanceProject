using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;

namespace SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;

public class AssignmentDTO
{
    public Guid Id { get; init; }

    public string Discipline { get; init; }

    public string DepartmentName { get; init; }

    public string TeacherName { get; init; }

    public string TeacherSurname { get; init; }

    public string? TeacherPatronymic { get; init; }

    public List<StudentAssignmentDTO> StudentAssignments { get; init; }

    public double AssignmentAverage { get; init; }

    public double AssignmentPerfomance { get; init; }

    public AssignmentDTO(Assignment assignment)
    {
        Id = assignment.Id;
        Discipline = assignment.Discipline.Discipline.Name;
        DepartmentName = assignment.Discipline.Teacher!.Department.Name.Name;
        TeacherName = assignment.Discipline.Teacher!.Name.Name;
        TeacherSurname = assignment.Discipline.Teacher!.Name.Surname;
        TeacherPatronymic = assignment.Discipline.Teacher!.Name.Patronymic;
        StudentAssignments = assignment
            .StudentAssignments.Select(a => new StudentAssignmentDTO(a))
            .OrderBy(a => a.StudentSurname)
            .ToList();
        DisciplinePerfomanceService service = new DisciplinePerfomanceService(assignment);
        AssignmentAverage = service.CalculateAssignmentAverage();
        AssignmentPerfomance = service.CalculateAssignmentPerfomance();
    }
}
