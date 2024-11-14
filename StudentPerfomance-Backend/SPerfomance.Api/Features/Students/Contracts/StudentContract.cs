namespace SPerfomance.Api.Features.Students.Contracts;

public class StudentContract
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Patronymic { get; set; }

    public string? State { get; set; }

    public ulong? Recordbook { get; set; }

    public string? GroupName { get; set; }
}
