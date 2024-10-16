using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Semester.Module.Api.Requests;
using SPerfomance.Application.Semester.Module.Queries.EducationPlanSemestersRequest;
using SPerfomance.Application.Semester.Module.Queries.Search;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

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
		if (request.Plan == null)
			return new BadRequestObjectResult(new EducationPlanNotFoundError().ToString());

		EducationPlanSchema plan = request.Plan.ToSchema();
		GetSemestersByEducationPlanQuery query = new GetSemestersByEducationPlanQuery(plan);
		OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
