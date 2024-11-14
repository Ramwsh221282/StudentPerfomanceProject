using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Queries.GetDepartmentByName;

public class GetDepartmentByNameQuery(string? name) : IQuery<TeachersDepartments>
{
    public string Name { get; init; } = name.ValueOrEmpty();
}
