using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationDirections.Module.Queries.All;
using SPerfomance.Application.EducationDirections.Module.Queries.Count;
using SPerfomance.Application.EducationDirections.Module.Queries.Filter;
using SPerfomance.Application.EducationDirections.Module.Queries.GetPaged;
using SPerfomance.Application.EducationDirections.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Api;

[ApiController]
[Route("/education-directions/api/read")]
public sealed class EducationDirectionsReadApi : ControllerBase
{
	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> GetAll()
	{
		GetAllDirectionsQuery query = new GetAllDirectionsQuery();
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> Count()
	{
		GetDirectionsCountQuery query = new GetDirectionsCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedEducationDirectionsQuery query = new GetPagedEducationDirectionsQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> Filter([FromQuery] EducationDirectionSchema schema, int page, int pageSize)
	{
		FilterQuery query = new FilterQuery(schema, page, pageSize);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<EducationDirectionSchema>>> Search([FromQuery] EducationDirectionSchema schema)
	{
		SearchQuery query = new SearchQuery(schema);
		OperationResult<IReadOnlyCollection<EducationDirection>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
