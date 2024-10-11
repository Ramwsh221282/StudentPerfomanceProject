using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.EducationPlans.Module.Api.Requests;
using SPerfomance.Application.EducationPlans.Module.Queries.All;
using SPerfomance.Application.EducationPlans.Module.Queries.Count;
using SPerfomance.Application.EducationPlans.Module.Queries.GetFiltered;
using SPerfomance.Application.EducationPlans.Module.Queries.GetPaged;
using SPerfomance.Application.EducationPlans.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api;

[ApiController]
[Route("/education-plans/api/read")]
public sealed class EducationPlansReadApi : ControllerBase
{
	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount()
	{
		GetCountQuery query = new GetCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> GetAll()
	{
		GetAllQuery query = new GetAllQuery();
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedQuery query = new GetPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> Filter([FromQuery] FilterRequest request)
	{
		GetFilteredQuery query = new GetFilteredQuery(request.Plan, request.Page, request.PageSize);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> Search([FromQuery] EducationPlanSchema plan)
	{
		SearchQuery query = new SearchQuery(plan);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
