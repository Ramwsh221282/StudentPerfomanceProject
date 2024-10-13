using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Api.Requests;
using SPerfomance.Application.StudentGroups.Module.Queries.All;
using SPerfomance.Application.StudentGroups.Module.Queries.Count;
using SPerfomance.Application.StudentGroups.Module.Queries.Filter;
using SPerfomance.Application.StudentGroups.Module.Queries.Paged;
using SPerfomance.Application.StudentGroups.Module.Queries.Search;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Api;

[ApiController]
[Route("/student-groups/api/read")]
public sealed class StudentGroupsReadApi : Controller
{
	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount()
	{
		GetCountQuery query = new GetCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<StudentsGroupSchema>>> GetAll()
	{
		GetAllQuery query = new GetAllQuery();
		OperationResult<IReadOnlyCollection<StudentGroup>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<StudentsGroupSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedQuery query = new GetPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<StudentGroup>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<StudentsGroupSchema>>> Filter([FromQuery] StudentGroupsFilterRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		StudentsGroupSchema group = request.Group.ToSchema();
		FilterQuery query = new FilterQuery(group, page, pageSize);
		OperationResult<IReadOnlyCollection<StudentGroup>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<StudentsGroupSchema>>> Search([FromQuery] StudentGroupsSearchRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		SearchQuery query = new SearchQuery(group);
		OperationResult<IReadOnlyCollection<StudentGroup>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
