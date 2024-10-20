using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.Students;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Api.Request;
using SPerfomance.Application.Students.Module.Commands.Create;
using SPerfomance.Application.Students.Module.Commands.Remove;
using SPerfomance.Application.Students.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Api;

[ApiController]
[Route("/student/api/management")]
public class StudentManagementApi : Controller
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<StudentSchema>> Create([FromBody] CreateStudentRequest request)
	{
		StudentSchema student = request.Student.ToSchema();
		string token = request.Token;
		CreateCommand command = new CreateCommand(student, token);
		OperationResult<Student> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<StudentSchema>> Remove([FromBody] RemoveStudentRequest request)
	{
		StudentSchema student = request.Student.ToSchema();
		string token = request.Token;
		RemoveCommand command = new RemoveCommand(student, token);
		OperationResult<Student> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<StudentSchema>> Update([FromBody] UpdateStudentRequest request)
	{
		StudentSchema initial = request.Initial.ToSchema();
		StudentSchema updated = request.Updated.ToSchema();
		string token = request.Token;
		UpdateCommand command = new UpdateCommand(initial, updated, token);
		OperationResult<Student> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
