using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.SemesterPlans.Module.Api.Requests;
using SPerfomance.Application.SemesterPlans.Module.Queries.All;
using SPerfomance.Application.SemesterPlans.Module.Queries.Count;
using SPerfomance.Application.SemesterPlans.Module.Queries.Filter;
using SPerfomance.Application.SemesterPlans.Module.Queries.GetBySemester;
using SPerfomance.Application.SemesterPlans.Module.Queries.Paged;
using SPerfomance.Application.SemesterPlans.Module.Queries.Searched;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Api;

[ApiController]
[Route("/semester-plans/api/read")]
public sealed class SemesterPlanReadApi
{
	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount()
	{
		GetCountQuery query = new GetCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedQuery query = new GetPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> GetPaged()
	{
		GetAllQuery query = new GetAllQuery();
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> GetFiltered([FromQuery] SemesterPlanSchema schema, int page, int pageSize)
	{
		FilterQuery query = new FilterQuery(schema, page, pageSize);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> Search([FromQuery] SemesterPlanSchema schema)
	{
		SearchQuery query = new SearchQuery(schema);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("/semester-plans/api/read/by-semester")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> GetBySemester([FromQuery] GetSemesterDisciplines request)
	{
		GetBySemesterQuery query = new GetBySemesterQuery(request.Semester);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
