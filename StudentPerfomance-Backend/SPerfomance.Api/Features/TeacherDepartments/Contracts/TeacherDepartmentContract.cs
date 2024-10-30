using SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;

namespace SPerfomance.Api.Features.TeacherDepartments.Contracts;

public class TeacherDepartmentContract
{
	public string? Name { get; set; }

	public static implicit operator CreateTeachersDepartmentCommand(TeacherDepartmentContract contract) =>
		new CreateTeachersDepartmentCommand(contract.Name);

	public static implicit operator GetDepartmentByNameQuery(TeacherDepartmentContract contract) =>
		new GetDepartmentByNameQuery(contract.Name);
}
