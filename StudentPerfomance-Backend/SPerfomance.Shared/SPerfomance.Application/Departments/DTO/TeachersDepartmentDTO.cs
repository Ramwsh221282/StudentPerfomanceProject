using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Application.Departments.DTO;

public class TeachersDepartmentDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Acronymus { get; set; }
    public int? EntityNumber { get; set; }
    public List<TeacherDto> Teachers { get; set; } = [];
}

public static class TeachersDepartmentDtoExtensions
{
    public static TeachersDepartmentDto MapFromDomain(this TeachersDepartments department) =>
        new()
        {
            Id = department.Id,
            Name = department.Name.Name,
            Acronymus = department.Acronymus,
            EntityNumber = department.EntityNumber,
            Teachers = department.Teachers.Select(t => t.MapFromDomain()).ToList(),
        };
}
