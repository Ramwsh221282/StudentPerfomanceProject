using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;
using SPerfomance.Application.Shared.Module.DTOs.Students;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Api.Request;
using SPerfomance.Application.Students.Module.Queries.GetGroupStudents;
using SPerfomance.Application.Students.Module.Queries.Search;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Api;

[ApiController]
[Route("/student/api/read")]
public sealed class StudentReadApi : ControllerBase
{
	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> Search([FromQuery] StudentSearchRequest request)
	{
		StudentSchema student = request.Student.ToSchema();
		string token = request.Token;
		SearchQuery query = new SearchQuery(student, token);
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("by-group")]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> GetByGroup([FromQuery] GetGroupStudentsRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		string token = request.Token;
		GetGroupStudentsQuery query = new GetGroupStudentsQuery(group, token);
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
