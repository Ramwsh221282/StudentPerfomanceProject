using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Application.Departments.DTO;

public class TeacherDTO
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public string? JobTitle { get; set; }

    public string? State { get; set; }

    public int? EntityNumber { get; set; }

    public TeachersDepartmentDTO? Department { get; set; }
}

public static class TeacherDTOExtensions
{
    public static TeacherDTO MapFromDomain(this Teacher teacher) =>
        new TeacherDTO()
        {
            Name = teacher.Name.Name,
            Surname = teacher.Name.Surname,
            Patronymic = teacher.Name.Patronymic,
            JobTitle = teacher.JobTitle.JobTitle,
            State = teacher.State.State,
            Department = teacher.Department.MapFromDomain(),
            EntityNumber = teacher.EntityNumber,
        };
}
