using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Semester.Module.Api.Requests;
using SPerfomance.Application.Semester.Module.Queries.EducationPlanSemestersRequest;
using SPerfomance.Application.Semester.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;

namespace SPerfomance.Application.Semester.Module.Api;

[ApiController]
[Route("/semesters/api/read")]
public sealed class SemesterReadApi : ControllerBase
{
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
