using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Facade.EducationDirectionsFacade;
using StudentPerfomance.Api.Requests.EducationDirectionRequests;
using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application.Queries.EducationDirections.FilterConstraints;

namespace StudentPerfomance.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class EducationDirectionsController : Controller
{
	[HttpPost]
	public async Task<ActionResult<EducationDirectionResponse>> CreateDirection([FromBody] CreateEducationDirectionRequest request) =>
		await new EducationDirectionCreateFacade(request).Process();

	[HttpDelete]
	public async Task<ActionResult<EducationDirectionResponse>> DeleteDirection([FromBody] DeleteEducationDirectionRequest request) =>
		await new EducationDirectionDeleteFacade(request).Process();

	[HttpPut]
	public async Task<ActionResult<EducationDirectionResponse>> UpdateDirection([FromBody] UpdateEducationDirectionNameRequest request) =>
		await new EducationDirectionUpdateNameFacade(request).Process();

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalCount() => await new EducationDirectionCountFacade().Process();

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> GetPaged(int page, int pageSize) =>
		await new EducationDirectionGetPagedFacade(page, pageSize).Process();

	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Search([FromQuery] FilterEducationDirectionRequest request) =>
		await new EducationDirectionSearchFacade(request, new FilterConstraint(FilterConstraint.General)).Process();

	[HttpGet("searchbyName")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> SearchByName([FromQuery] FilterEducationDirectionRequest request) =>
		await new EducationDirectionSearchByNameFacade(request, new FilterConstraint(FilterConstraint.NameOnly)).Process();

	[HttpGet("searchCode")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> SearchByCode([FromQuery] FilterEducationDirectionRequest request) =>
		await new EducationDirectionSearchByCodeFacade(request, new FilterConstraint(FilterConstraint.CodeOnly)).Process();

	[HttpGet("filter")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Filter([FromQuery] FilterEducationDirectionRequest request, int page, int pageSize) =>
		await new EducationDirectionFilterFacade(request, new FilterConstraint(FilterConstraint.General), page, pageSize).Process();

	[HttpGet("filterByName")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> FilterByName([FromQuery] FilterEducationDirectionRequest request, int page, int pageSize) =>
		await new EducationDirectionFilterByNameFacade(request, new FilterConstraint(FilterConstraint.NameOnly), page, pageSize).Process();

	[HttpGet("filterByCode")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> FilterByCode([FromQuery] FilterEducationDirectionRequest request, int page, int pageSize) =>
		await new EducationDirectionFilterByCodeFacade(request, new FilterConstraint(FilterConstraint.NameOnly), page, pageSize).Process();
}
