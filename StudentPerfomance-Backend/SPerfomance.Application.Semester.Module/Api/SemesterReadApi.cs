using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Semester.Module.Api.Requests;
using SPerfomance.Application.Semester.Module.Queries.All;
using SPerfomance.Application.Semester.Module.Queries.Count;
using SPerfomance.Application.Semester.Module.Queries.EducationPlanSemestersRequest;
using SPerfomance.Application.Semester.Module.Queries.Filter;
using SPerfomance.Application.Semester.Module.Queries.Paged;
using SPerfomance.Application.Semester.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;

namespace SPerfomance.Application.Semester.Module.Api;

[ApiController]
[Route("/semesters/api/read")]
public sealed class SemesterReadApi : ControllerBase
{
	[HttpGet(CrudOperationNames.GetAll)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterSchema>>> GetAll()
	{
		GetAllQuery query = new GetAllQuery();
		OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.GetCount)]
	public async Task<ActionResult<int>> Count()
	{
		GetCountQuery query = new GetCountQuery();
		OperationResult<int> result = await query.Handler.Handle(query);
		return new OkObjectResult(result.Result);
	}

	[HttpGet(CrudOperationNames.GetPaged)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterSchema>>> GetPaged(int page, int pageSize)
	{
		GetPagedQuery query = new GetPagedQuery(page, pageSize);
		OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Filter)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterSchema>>> Filter([FromQuery] SemesterSchema semester, int page, int pageSize)
	{
		FilterQuery query = new FilterQuery(semester, page, pageSize);
		OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterSchema>>> Search([FromQuery] SemesterSchema semester)
	{
		SearchQuery query = new SearchQuery(semester);
		OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("education-plan-semesters")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterSchema>>> GetByEducationPlan([FromQuery] EducationPlanSemestersRequest request)
	{
		GetSemestersByEducationPlanQuery query = new GetSemestersByEducationPlanQuery(request.schema);
		OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
