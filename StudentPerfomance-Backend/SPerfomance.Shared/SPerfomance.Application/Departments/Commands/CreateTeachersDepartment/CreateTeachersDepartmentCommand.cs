using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;

public class CreateTeachersDepartmentCommand(string? name) : ICommand<TeachersDepartments>
{
	public string Name { get; init; } = name.ValueOrEmpty();
}
