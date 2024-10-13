using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;
using SPerfomance.Application.Shared.Module.DTOs.Students;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.Students;
using SPerfomance.Application.Students.Module.Api.Request;
using SPerfomance.Application.Students.Module.Queries.All;
using SPerfomance.Application.Students.Module.Queries.Count;
using SPerfomance.Application.Students.Module.Queries.Filter;
using SPerfomance.Application.Students.Module.Queries.GetGroupStudents;
using SPerfomance.Application.Students.Module.Queries.Paged;
using SPerfomance.Application.Students.Module.Queries.Search;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Api;

[ApiController]
[Route("/student/api/read")]
public sealed class StudentReadApi : ControllerBase
{
	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> GetAll()
	{
		GetAllQuery query = new GetAllQuery();
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> GetCount()
	{
		GetCountQuery query = new GetCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedQuery query = new GetPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> Filter([FromQuery] StudentSchema student, int page, int pageSize)
	{
		FilterQuery query = new FilterQuery(student, page, pageSize);
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> Search([FromQuery] SearchStudentsRequest request)
	{
		StudentSchema student = request.Student.ToSchema();
		SearchQuery query = new SearchQuery(student);
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("by-group")]
	public async Task<ActionResult<IReadOnlyCollection<StudentSchema>>> GetByGroup([FromQuery] GetGroupStudentsRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		GetGroupStudentsQuery query = new GetGroupStudentsQuery(group);
		OperationResult<IReadOnlyCollection<Student>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
