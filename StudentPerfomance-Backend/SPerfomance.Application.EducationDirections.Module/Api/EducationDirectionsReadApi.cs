using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationDirections.Module.Api.Requests;
using SPerfomance.Application.EducationDirections.Module.Queries.All;
using SPerfomance.Application.EducationDirections.Module.Queries.Count;
using SPerfomance.Application.EducationDirections.Module.Queries.Filter;
using SPerfomance.Application.EducationDirections.Module.Queries.GetPaged;
using SPerfomance.Application.EducationDirections.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api;

[ApiController]
[Route("/education-directions/api/read")]
public sealed class EducationDirectionsReadApi : Controller
{
	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> GetAll([FromQuery] EducationDirectionDataRequest request)
	{
		string token = request.Token;
		GetAllDirectionsQuery query = new GetAllDirectionsQuery(token);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> Count([FromQuery] EducationDirectionDataRequest request)
	{
		string token = request.Token;
		GetDirectionsCountQuery query = new GetDirectionsCountQuery(token);
		OperationResult<int> result = await query.Handler.Handle(query);
		return result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> GetPaged([FromQuery] EducationDirectionPagedDataRequest request)
	{
		int page = request.Page;
		int pageSize = request.PageSize;
		string token = request.Token;
		GetPagedEducationDirectionsQuery query = new GetPagedEducationDirectionsQuery(page, pageSize, token);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> Filter([FromQuery] EducationDirectionFilterDataRequest request)
	{
		EducationDirectionDTO direction = request.Direction;
		int page = request.Page;
		int pageSize = request.PageSize;
		string token = request.Token;
		FilterQuery query = new FilterQuery(direction, page, pageSize, token);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> Search([FromQuery] EducationDirectionSearchDataRequest request)
	{
		EducationDirectionDTO direction = request.Direction;
		string token = request.Token;
		SearchQuery query = new SearchQuery(direction, token);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
