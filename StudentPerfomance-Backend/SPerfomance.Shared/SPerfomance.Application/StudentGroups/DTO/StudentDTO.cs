using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Application.StudentGroups.DTO;

public class StudentDto
{
    public Guid Id { get; set; }
    public int? EntityNumber { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? State { get; set; }
    public ulong? Recordbook { get; set; }
    public StudentGroupDto? Group { get; set; }
}

public static class StudentDtoExtensions
{
    public static StudentDto MapFromDomain(this Student student) =>
        new()
        {
            Id = student.Id,
            Name = student.Name.Name,
            Surname = student.Name.Surname,
            Patronymic = student.Name.Patronymic,
            State = student.State.State,
            Recordbook = student.Recordbook.Recordbook,
            Group = student.AttachedGroup.MapFromDomain(),
            EntityNumber = student.EntityNumber,
        };

    public static StudentDto MapFromDomainWithoutGroup(this Student student) =>
        new()
        {
            Id = student.Id,
            Name = student.Name.Name,
            Surname = student.Name.Surname,
            Patronymic = student.Name.Patronymic,
            State = student.State.State,
            Recordbook = student.Recordbook.Recordbook,
            EntityNumber = student.EntityNumber,
        };
}
