using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;

namespace StudentPerfomance.Application.Commands.Departments.DeleteDepartment;

internal sealed class DeleteDepartmentCommand(DepartmentSchema schema) : ICommand
{
	public DepartmentSchema Department { get; init; } = schema;
}
