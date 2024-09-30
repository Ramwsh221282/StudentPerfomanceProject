using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.EducationPlans;
using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public class EducationPlansController : Controller
{
	[HttpPost]
	public async Task<ActionResult<EducationPlanResponse>> CreateEducationPlan([FromBody] EducationPlanSchema request)
	{
		EducationPlanCreateFacade facade = new EducationPlanCreateFacade(request);
		return await facade.Process();
	}

	[HttpDelete]
	public async Task<ActionResult<EducationPlanResponse>> DeleteEducationPlan([FromBody] EducationPlanSchema request)
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
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> GetEducationPlansPaged(int page, int pageSize)
	{
		EducationPlanGetPagedFacade facade = new EducationPlanGetPagedFacade(page, pageSize);
		return await facade.Process();
	}

	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> SearchEducationPlans([FromQuery] EducationPlanSchema request)
	{
		EducationPlanSearchFacade facade = new EducationPlanSearchFacade(request);
		return await facade.Process();
	}

	[HttpGet("filter")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> FilterEducationPlans
	(
		[FromQuery] EducationPlanSchema request,
		int page,
		int pageSize
	)
	{
		EducationPlanFilterFacade facade = new EducationPlanFilterFacade(request, page, pageSize);
		return await facade.Process();
	}
}
