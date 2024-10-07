using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Create;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Api;

[ApiController]
[Route("/teacher-departments/api/management")]
public sealed class TeacherDepartmentsManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<DepartmentSchema>> Create([FromBody] DepartmentSchema department)
	{
		TeacherDepartmentCreateCommand command = new TeacherDepartmentCreateCommand(department);
		OperationResult<TeachersDepartment> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<DepartmentSchema>> Delete([FromBody] DepartmentSchema department)
	{
		TeacherDepartmentDeleteCommand command = new TeacherDepartmentDeleteCommand(department);
		OperationResult<TeachersDepartment> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<DepartmentSchema>> Update([FromQuery] DepartmentSchema oldSchema, [FromQuery] DepartmentSchema newSchema)
	{
		TeachersDepartmentUpdateCommand command = new TeachersDepartmentUpdateCommand(newSchema, oldSchema);
		OperationResult<TeachersDepartment> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
