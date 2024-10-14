using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Application.Teachers.Module.Commands.Create;
using SPerfomance.Application.Teachers.Module.Commands.Remove;
using SPerfomance.Application.Teachers.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Api;

[ApiController]
[Route("/teacher/api/management")]
public sealed class TeacherManagementApi : Controller
{
	[HttpGet(CrudOperationNames.Create)]
	public async Task<ActionResult<TeacherSchema>> Create([FromBody] TeacherSchema teacher)
	{
		CreateCommand command = new CreateCommand(teacher);
		OperationResult<Teacher> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<TeacherSchema>> Remove([FromBody] TeacherSchema teacher)
	{
		RemoveCommand command = new RemoveCommand(teacher);
		OperationResult<Teacher> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<TeacherSchema>> Update([FromQuery] TeacherSchema oldSchema, [FromQuery] TeacherSchema newSchema)
	{
		UpdateCommand command = new UpdateCommand(oldSchema, newSchema);
		OperationResult<Teacher> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
