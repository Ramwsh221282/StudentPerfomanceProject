using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Application.StudentGroups.DTO;

public class StudentDTO
{
    public int? EntityNumber { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public string? State { get; set; }

    public ulong? Recordbook { get; set; }

    public StudentGroupDTO? Group { get; set; }
}

public static class StudentDTOExtensions
{
    public static StudentDTO MapFromDomain(this Student student) =>
        new StudentDTO()
        {
            Name = student.Name.Name,
            Surname = student.Name.Surname,
            Patronymic = student.Name.Patronymic,
            State = student.State.State,
            Recordbook = student.Recordbook.Recordbook,
            Group = student.AttachedGroup.MapFromDomain(),
            EntityNumber = student.EntityNumber,
        };
}
