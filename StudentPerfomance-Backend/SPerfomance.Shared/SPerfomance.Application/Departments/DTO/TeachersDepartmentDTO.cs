using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Application.Departments.DTO;

public class TeachersDepartmentDTO
{
    public string? Name { get; set; }

    public string? Acronymus { get; set; }

    public int? EntityNumber { get; set; }
}

public static class TeachersDepartmentDTOExtensions
{
    public static TeachersDepartmentDTO MapFromDomain(this TeachersDepartments department) =>
        new TeachersDepartmentDTO()
        {
            Name = department.Name.Name,
            Acronymus = department.Acronymus,
            EntityNumber = department.EntityNumber,
        };
}
