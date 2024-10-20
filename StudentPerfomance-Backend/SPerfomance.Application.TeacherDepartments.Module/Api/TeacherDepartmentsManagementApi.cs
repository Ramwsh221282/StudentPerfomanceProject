using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.Departments;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.TeacherDepartments.Module.Api.Requests;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Create;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;
using SPerfomance.Application.TeacherDepartments.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Api;

[ApiController]
[Route("/teacher-departments/api/management")]
public sealed class TeacherDepartmentsManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<DepartmentSchema>> Create([FromBody] DepartmentCreateRequest request)
	{
		if (request == null)
			return new BadRequestObjectResult(new DepartmentNameError().ToString());

		DepartmentSchema department = request.Department.ToSchema();
		string token = request.Token;
		TeacherDepartmentCreateCommand command = new TeacherDepartmentCreateCommand(department, token);
		OperationResult<TeachersDepartment> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<DepartmentSchema>> Delete([FromBody] DepartmentRemoveRequest request)
	{
		if (request == null)
			return new BadRequestObjectResult(new DepartmentNotFountError().ToString());

		DepartmentSchema department = request.Department.ToSchema();
		string token = request.Token;
		TeacherDepartmentDeleteCommand command = new TeacherDepartmentDeleteCommand(department, token);
		OperationResult<TeachersDepartment> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<DepartmentSchema>> Update([FromBody] DepartmentUpdateRequest request)
	{
		if (request == null)
			return new BadRequestObjectResult(new DepartmentNotFountError().ToString());

		DepartmentSchema initial = request.Initial.ToSchema();
		DepartmentSchema updated = request.Updated.ToSchema();
		string token = request.Token;
		TeachersDepartmentUpdateCommand command = new TeachersDepartmentUpdateCommand(updated, initial, token);
		OperationResult<TeachersDepartment> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
