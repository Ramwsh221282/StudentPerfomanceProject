using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationPlans.Module.Api.Requests;
using SPerfomance.Application.EducationPlans.Module.Queries.All;
using SPerfomance.Application.EducationPlans.Module.Queries.Count;
using SPerfomance.Application.EducationPlans.Module.Queries.GetFiltered;
using SPerfomance.Application.EducationPlans.Module.Queries.GetPaged;
using SPerfomance.Application.EducationPlans.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api;

[ApiController]
[Route("/education-plans/api/read")]
public sealed class EducationPlansReadApi : ControllerBase
{
	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> GetCount([FromQuery] EducationPlanDataRequest request)
	{
		string token = request.Token;
		GetCountQuery query = new GetCountQuery(token);
		OperationResult<int> result = await query.Handler.Handle(query);
		return result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> GetAll([FromQuery] EducationPlanDataRequest request)
	{
		string token = request.Token;
		GetAllQuery query = new GetAllQuery(token);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> GetPaged([FromQuery] EducationPlanPagedDataRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		string token = request.Token;
		GetPagedQuery query = new GetPagedQuery(page, pageSize, token);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> Filter([FromQuery] EducationPlanFilterRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		string token = request.Token;
		EducationPlanDTO plan = request.Plan;
		GetFilteredQuery query = new GetFilteredQuery(plan, page, pageSize, token);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanSchema>>> Search([FromQuery] EducationPlanSearchRequest request)
	{
		EducationPlanDTO plan = request.Plan;
		string token = request.Token;
		SearchQuery query = new SearchQuery(plan, token);
		OperationResult<IReadOnlyCollection<EducationPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
