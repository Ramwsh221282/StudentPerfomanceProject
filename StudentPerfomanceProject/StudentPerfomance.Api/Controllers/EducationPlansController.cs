using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Facade.EducationPlansFacade;
using StudentPerfomance.Api.Requests.EducationPlanRequests;
using StudentPerfomance.Api.Responses.EducationPlans;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class EducationPlansController : Controller
{
	[HttpPost]
	public async Task<ActionResult<EducationPlanResponse>> CreateEducationPlan([FromBody] EducationPlanGeneralRequest request) =>
		await new EducationPlanCreateFacade(request).Process();

	[HttpDelete]
	public async Task<ActionResult<EducationPlanResponse>> DeleteEducationPlan([FromBody] EducationPlanGeneralRequest request) =>
	 	await new EducationPlanDeleteFacade(request).Process();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetEducationPlansCount() => await new EducationPlanCountFacade().Process();

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> GetEducationPlansPaged(int page, int pageSize) =>
		await new EducationPlanGetPagedFacade(page, pageSize).Process();

	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> SearchEducationPlans([FromQuery] EducationPlanGeneralRequest request) =>
		await new EducationPlanSearchFacade(request).Process();

	[HttpGet("searchByYear")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> SearchEducationPlansByYear([FromQuery] EducationPlanGeneralRequest request) =>
		await new EducationPlanSearchByYearFacade(request).Process();

	[HttpGet("searchByDirection")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> SearchEducationPlansByDirection([FromQuery] EducationPlanGeneralRequest request) =>
		await new EducationPlanSearchByDirectionFacade(request).Process();

	[HttpGet("filter")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> FilterEducationPlans([FromQuery] EducationPlanGeneralRequest request, int page, int pageSize) =>
		await new EducationPlanFilterFacade(request, page, pageSize).Process();
	[HttpGet("filterByYear")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> FilterEducationPlansByYear([FromQuery] EducationPlanGeneralRequest request, int page, int pageSize) =>
		await new EducationPlanFilterByYearFacade(request, page, pageSize).Process();
	[HttpGet("filterByDirection")]
	public async Task<ActionResult<IReadOnlyCollection<EducationPlanResponse>>> FilterEducationPlansByDirection([FromQuery] EducationPlanGeneralRequest request, int page, int pageSize) =>
		await new EducationPlanFilterByDirectionFacade(request, page, pageSize).Process();
}
