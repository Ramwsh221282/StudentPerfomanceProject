using SPerfomance.Domain.Models.Teachers;

namespace SPerfomance.Application.Departments.DTO;

public class TeacherDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? JobTitle { get; set; }
    public string? State { get; set; }
    public int? EntityNumber { get; set; }
}

public static class TeacherDtoExtensions
{
    public static TeacherDto MapFromDomain(this Teacher teacher) =>
        new()
        {
            Id = teacher.Id,
            Name = teacher.Name.Name,
            Surname = teacher.Name.Surname,
            Patronymic = teacher.Name.Patronymic,
            JobTitle = teacher.JobTitle.JobTitle,
            State = teacher.State.State,
            EntityNumber = teacher.EntityNumber,
        };
}
