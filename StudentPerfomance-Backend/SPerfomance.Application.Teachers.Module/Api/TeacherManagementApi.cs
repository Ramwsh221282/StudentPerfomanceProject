using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.Teachers;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Api.Requests;
using SPerfomance.Application.Teachers.Module.Commands.Create;
using SPerfomance.Application.Teachers.Module.Commands.Remove;
using SPerfomance.Application.Teachers.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api;

[ApiController]
[Route("/teacher/api/management")]
public sealed class TeacherManagementApi : Controller
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<TeacherSchema>> Create([FromBody] CreateTeacherRequest request)
	{
		TeacherSchema teacher = request.Teacher.ToSchema();
		string token = request.Token;
		CreateCommand command = new CreateCommand(teacher, token);
		OperationResult<Teacher> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<TeacherSchema>> Remove([FromBody] RemoveTeacherRequest request)
	{
		TeacherSchema teacher = request.Teacher.ToSchema();
		string token = request.Token;
		RemoveCommand command = new RemoveCommand(teacher, token);
		OperationResult<Teacher> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<TeacherSchema>> Update([FromBody] UpdateTeacherRequest request)
	{
		TeacherSchema initial = request.Initial.ToSchema();
		TeacherSchema updated = request.Updated.ToSchema();
		string token = request.Token;
		UpdateCommand command = new UpdateCommand(initial, updated, token);
		OperationResult<Teacher> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
