using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.SemesterPlans.Module.Api.Requests;
using SPerfomance.Application.SemesterPlans.Module.Queries.GetBySemester;
using SPerfomance.Application.SemesterPlans.Module.Queries.Searched;
using SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;
using SPerfomance.Application.Shared.Module.DTOs.Semesters;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

namespace SPerfomance.Application.SemesterPlans.Module.Api;

[ApiController]
[Route("/semester-plans/api/read")]
public sealed class SemesterPlanReadApi
{
	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> Search([FromQuery] SemesterPlanQuery request)
	{
		string token = request.Token;
		SemesterPlanSchema schema = request.SemesterPlan.ToSchema();
		SearchQuery query = new SearchQuery(schema, token);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("/semester-plans/api/read/by-semester")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> GetBySemester([FromQuery] GetSemesterDisciplines request)
	{
		if (request.Semester == null)
			return new BadRequestObjectResult(new SemesterNotFoundError());

		string token = request.Token;
		SemesterSchema semester = request.Semester.ToSchema();
		GetBySemesterQuery query = new GetBySemesterQuery(semester, token);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
