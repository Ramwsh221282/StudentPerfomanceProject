using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Commands.Create;
using SPerfomance.Application.StudentGroups.Module.Commands.Delete;
using SPerfomance.Application.StudentGroups.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api;

[ApiController]
[Route("/student-groups/api/management")]
public sealed class StudentGroupsManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<StudentsGroupSchema>> Create([FromBody] StudentsGroupSchema group)
	{
		CreateCommand command = new CreateCommand(group);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<StudentsGroupSchema>> Remove([FromBody] StudentsGroupSchema group)
	{
		DeleteCommand command = new DeleteCommand(group);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<StudentsGroupSchema>> Update([FromQuery] StudentsGroupSchema oldSchema, [FromQuery] StudentsGroupSchema newSchema)
	{
		UpdateCommand command = new UpdateCommand(oldSchema, newSchema);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
