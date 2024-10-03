using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;

using Response = Microsoft.AspNetCore.Mvc.ActionResult<SPerfomance.Api.Module.Responses.EducationDirections.EducationDirectionResponse>;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.EducationDirections.EducationDirectionResponse>>;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections.Expressions;
using SPerfomance.Application.EducationDirections.Module.Commands.Create;
using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Application.EducationDirections.Module.Commands.Delete;
using SPerfomance.Application.EducationDirections.Module.Commands.Update;
using SPerfomance.Application.EducationDirections.Module.Queries.All;
using SPerfomance.Application.EducationDirections.Module.Queries.Count;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Application.EducationDirections.Module.Queries.ByPage;
using SPerfomance.Application.EducationDirections.Module.Queries.GetFiltered;
using SPerfomance.Application.EducationDirections.Module.Queries.GetPagedFiltered;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class EducationDirectionsController : Controller
{
	private readonly IRepository<EducationDirection> _repository;
	public EducationDirectionsController(IRepository<EducationDirection> repository)
	{
		_repository = repository;
	}

	[HttpPost]
	public async Task<Response> CreateDirection([FromBody] EducationDirectionSchema request)
	{
		EducationDirectionsRepositoryObject parameter = request.ToRepositoryObject();
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FindDirectionByCode(parameter);
		CreateEducationDirectionCommand command = new CreateEducationDirectionCommand(request, expression, _repository);
		return EducationDirectionResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpDelete]
	public async Task<Response> DeleteDirection([FromBody] EducationDirectionSchema request)
	{
		EducationDirectionsRepositoryObject parameter = request.ToRepositoryObject();
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FindDirection(parameter);
		DeleteEducationDirectionCommand command = new DeleteEducationDirectionCommand(expression, _repository);
		return EducationDirectionResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpPut]
	public async Task<Response> UpdateDirection([FromQuery] EducationDirectionSchema oldDirection, [FromQuery] EducationDirectionSchema newDirection)
	{
		EducationDirectionsRepositoryObject oldParameter = oldDirection.ToRepositoryObject();
		EducationDirectionsRepositoryObject newParameter = newDirection.ToRepositoryObject();
		IRepositoryExpression<EducationDirection> findInitial = EducationDirectionExpressionsFactory.FindDirection(oldParameter);
		IRepositoryExpression<EducationDirection> codeCheck = EducationDirectionExpressionsFactory.FindDirectionByCode(newParameter);
		UpdateEducationDirectionCommand command = new UpdateEducationDirectionCommand(newDirection, findInitial, codeCheck, _repository);
		return EducationDirectionResponse.FromResult(await command.Handler.Handle(command));
	}

	[HttpGet("all")]
	public async Task<CollectionResponse> GetAll()
	{
		GetAllEducationDirectionsQuery query = new GetAllEducationDirectionsQuery(_repository);
		return EducationDirectionResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> GetTotalCount()
	{
		GetEducationDirectionsCountQuery query = new GetEducationDirectionsCountQuery(_repository);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetPaged(int page, int pageSize)
	{
		GetPagedEducationDirectionsQuery query = new GetPagedEducationDirectionsQuery(page, pageSize, _repository);
		return EducationDirectionResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> Search([FromQuery] EducationDirectionSchema request)
	{
		EducationDirectionsRepositoryObject parameter = request.ToRepositoryObject();
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterExpression(parameter);
		FilterEducationDirectionsQuery query = new FilterEducationDirectionsQuery(expression, _repository);
		return EducationDirectionResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("filter")]
	public async Task<CollectionResponse> Filter([FromQuery] EducationDirectionSchema request, int page, int pageSize)
	{
		EducationDirectionsRepositoryObject parameter = request.ToRepositoryObject();
		IRepositoryExpression<EducationDirection> expression = EducationDirectionExpressionsFactory.FilterExpression(parameter);
		GetPagedFilteredEducationDirectionsQuery query = new GetPagedFilteredEducationDirectionsQuery(page, pageSize, expression, _repository);
		return EducationDirectionResponse.FromResult(await query.Handler.Handle(query));
	}
}
