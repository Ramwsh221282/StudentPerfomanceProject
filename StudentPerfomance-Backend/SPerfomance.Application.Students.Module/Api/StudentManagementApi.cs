using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Commands.Create;
using SPerfomance.Application.Students.Module.Commands.Remove;
using SPerfomance.Application.Students.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Api;

[ApiController]
[Route("/student/api/management")]
public class StudentManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<StudentSchema>> Create([FromBody] StudentSchema student)
	{
		CreateCommand command = new CreateCommand(student);
		OperationResult<Student> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<StudentSchema>> Remove([FromBody] StudentSchema student)
	{
		RemoveCommand command = new RemoveCommand(student);
		OperationResult<Student> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<StudentSchema>> Update([FromQuery] StudentSchema oldSchema, [FromQuery] StudentSchema newSchema)
	{
		UpdateCommand command = new UpdateCommand(oldSchema, newSchema);
		OperationResult<Student> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
