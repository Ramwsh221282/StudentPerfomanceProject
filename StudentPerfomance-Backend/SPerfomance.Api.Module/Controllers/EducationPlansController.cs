using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.EducationPlans.EducationPlanResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.EducationPlans.EducationPlanResponse>>;
using SPerfomance.Application.EducationPlans.Module.Commands.Create;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Application.EducationPlans.Module.Commands.Delete;
using SPerfomance.Application.EducationPlans.Module.Queries.Count;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Application.EducationPlans.Module.Queries.ByPage;
using SPerfomance.Application.EducationPlans.Module.Queries.Search;
using SPerfomance.Application.EducationPlans.Module.Queries.PagedFilters;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public class EducationPlansController : Controller
{
	private readonly IRepository<EducationPlan> _plans;
	private readonly IRepository<EducationDirection> _directions;
	private readonly IRepository<Semester> _semesters;
	public EducationPlansController(IRepository<EducationPlan> plans, IRepository<EducationDirection> directions, IRepository<Semester> semesters)
	{
		_plans = plans;
		_directions = directions;
		_semesters = semesters;
	}
	[HttpPost]
	public async Task<Response> CreateEducationPlan([FromBody] EducationPlanSchema request)
	{
		EducationPlansRepositoryObject planParameter = request.ToRepositoryObject();
		EducationDirectionsRepositoryObject directionParameter = request.Direction.ToRepositoryObject();
		IRepositoryExpression<EducationPlan> findDublicate = EducationPlanExpressionsFactory.CreateFindPlan(planParameter);
		IRepositoryExpression<EducationDirection> findDirection = EducationDirectionExpressionsFactory.FindDirection(directionParameter);
		CreateEducationPlanCommand command = new CreateEducationPlanCommand(request, findDublicate, findDirection, _plans, _directions, _semesters);
		return EducationPlanResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpDelete]
	public async Task<Response> DeleteEducationPlan([FromBody] EducationPlanSchema request)
	{
		EducationPlansRepositoryObject paramter = request.ToRepositoryObject();
		IRepositoryExpression<EducationPlan> getDirection = EducationPlanExpressionsFactory.CreateFindPlan(paramter);
		DeleteEducationPlanCommand command = new DeleteEducationPlanCommand(getDirection, _plans);
		return EducationPlanResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetEducationPlansCount()
	{
		GetEducationPlansCountQuery query = new GetEducationPlansCountQuery(_plans);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetEducationPlansPaged(int page, int pageSize)
	{
		GetEducationPlansPagedQuery query = new GetEducationPlansPagedQuery(page, pageSize, _plans);
		return EducationPlanResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> SearchEducationPlans([FromQuery] EducationPlanSchema request)
	{
		EducationPlansRepositoryObject parameter = request.ToRepositoryObject();
		IRepositoryExpression<EducationPlan> expression = EducationPlanExpressionsFactory.CreateFilter(parameter);
		SearchEducationPlansQuery query = new SearchEducationPlansQuery(expression, _plans);
		return EducationPlanResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> FilterEducationPlans([FromQuery] EducationPlanSchema request, int page, int pageSize)
	{
		EducationPlansRepositoryObject parameter = request.ToRepositoryObject();
		IRepositoryExpression<EducationPlan> expression = EducationPlanExpressionsFactory.CreateFilter(parameter);
		FilterEducationPlansQuery query = new FilterEducationPlansQuery(page, pageSize, expression, _plans);
		return EducationPlanResponse.FromResult(await query.Handler.Handle(query));
	}
}
