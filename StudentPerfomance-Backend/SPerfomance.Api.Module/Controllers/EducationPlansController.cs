using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.EducationPlans.EducationPlanResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.EducationPlans.EducationPlanResponse>>;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public class EducationPlansController : Controller
{
	[HttpPost]
	public async Task<Response> CreateEducationPlan([FromBody] EducationPlanSchema request)
	{
		EducationPlanCreateFacade facade = new EducationPlanCreateFacade(request);
		return await facade.Process();
	}

	[HttpDelete]
	public async Task<Response> DeleteEducationPlan([FromBody] EducationPlanSchema request)
	{
		EducationPlanDeleteFacade facade = new EducationPlanDeleteFacade(request);
		return await facade.Process();
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetEducationPlansCount()
	{
		EducationPlanCountFacade facade = new EducationPlanCountFacade();
		return await facade.Process();
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetEducationPlansPaged(int page, int pageSize)
	{
		EducationPlanGetPagedFacade facade = new EducationPlanGetPagedFacade(page, pageSize);
		return await facade.Process();
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> SearchEducationPlans([FromQuery] EducationPlanSchema request)
	{
		EducationPlanSearchFacade facade = new EducationPlanSearchFacade(request);
		return await facade.Process();
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> FilterEducationPlans([FromQuery] EducationPlanSchema request, int page, int pageSize)
	{
		EducationPlanFilterFacade facade = new EducationPlanFilterFacade(request, page, pageSize);
		return await facade.Process();
	}
}
