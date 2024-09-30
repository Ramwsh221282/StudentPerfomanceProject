using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Facades.EducationDirections;
using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class EducationDirectionsController : Controller
{
	[HttpPost]
	public async Task<ActionResult<EducationDirectionResponse>> CreateDirection([FromBody] EducationDirectionSchema request)
	{
		EducationDirectionCreateFacade facade = new EducationDirectionCreateFacade(request);
		return await facade.Process();
	}

	[HttpDelete]
	public async Task<ActionResult<EducationDirectionResponse>> DeleteDirection([FromBody] EducationDirectionSchema request)
	{
		EducationDirectionDeleteFacade facade = new EducationDirectionDeleteFacade(request);
		return await facade.Process();
	}

	[HttpPut]
	public async Task<ActionResult<EducationDirectionResponse>> UpdateDirection
	(
		[FromQuery] EducationDirectionSchema oldDirection,
		[FromQuery] EducationDirectionSchema newDirection
	)
	{
		EducationDirectionUpdateFacade facade = new EducationDirectionUpdateFacade(oldDirection, newDirection);
		return await facade.Process();
	}

	[HttpGet("all")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> GetAll()
	{
		EducationDirectionGetAllFacade facade = new EducationDirectionGetAllFacade();
		return await facade.Process();
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalCount()
	{
		EducationDirectionCountFacade facade = new EducationDirectionCountFacade();
		return await facade.Process();
	}

	[HttpGet("byPage")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> GetPaged(int page, int pageSize)
	{
		EducationDirectionGetPagedFacade facade = new EducationDirectionGetPagedFacade(page, pageSize);
		return await facade.Process();
	}

	[HttpGet("search")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Search([FromQuery] EducationDirectionSchema request)
	{
		EducationDirectionSearchFacade facade = new EducationDirectionSearchFacade(request);
		return await facade.Process();
	}

	[HttpGet("filter")]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionResponse>>> Filter([FromQuery] EducationDirectionSchema request, int page, int pageSize)
	{
		EducationDirectionFilterFacade facade = new EducationDirectionFilterFacade(request, page, pageSize);
		return await facade.Process();
	}
}
