using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Module.Converters.Semesters;
using SPerfomance.Api.Module.Responses.Semesters;
using SPerfomance.Application.Semesters.Module.Queries.GetCount;
using SPerfomance.Application.Semesters.Module.Queries.GetPaged;
using SPerfomance.Application.Semesters.Module.Queries.GetPagedFiltered;
using SPerfomance.Application.Semesters.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters.Expressions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using CollectionResponse = Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IReadOnlyCollection<SPerfomance.Api.Module.Responses.Semesters.SemesterResponse>>;

namespace SPerfomance.Api.Module.Controllers;

[ApiController]
[Route("[controller]")]
public class SemestersController : Controller
{
	private readonly IRepository<Semester> _repository;

	public SemestersController(IRepository<Semester> repository)
	{
		_repository = repository;
	}

	[HttpGet("byPage")]
	public async Task<CollectionResponse> GetByPage(int page, int pageSize)
	{
		GetPagedSemestersQuery query = new GetPagedSemestersQuery(page, pageSize, _repository);
		return SemesterResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("totalCount")]
	public async Task<ActionResult<int>> TotalCount()
	{
		GetSemestersCountQuery query = new GetSemestersCountQuery(_repository);
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result);
	}

	[HttpGet("byFilter")]
	public async Task<CollectionResponse> GetPagedByFilter([FromQuery] SemesterSchema semester, int page, int pageSize)
	{
		SemestersRepositoryObject parameter = semester.ToRepositoryObject();
		IRepositoryExpression<Semester> expression = SemesterExpressionsFactory.CreateFilter(parameter);
		FilterSemestersQuery query = new FilterSemestersQuery(page, pageSize, expression, _repository);
		return SemesterResponse.FromResult(await query.Handler.Handle(query));
	}

	[HttpGet("search")]
	public async Task<CollectionResponse> GetBySearch([FromQuery] SemesterSchema semester)
	{
		SemestersRepositoryObject parameter = semester.ToRepositoryObject();
		IRepositoryExpression<Semester> expression = SemesterExpressionsFactory.CreateFilter(parameter);
		SearchSemestersQuery query = new SearchSemestersQuery(expression, _repository);
		return SemesterResponse.FromResult(await query.Handler.Handle(query));
	}
}
