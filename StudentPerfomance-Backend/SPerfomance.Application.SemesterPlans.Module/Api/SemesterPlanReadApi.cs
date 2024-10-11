using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.SemesterPlans.Module.Api.Requests;
using SPerfomance.Application.SemesterPlans.Module.Queries.GetBySemester;
using SPerfomance.Application.SemesterPlans.Module.Queries.Searched;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Api;

[ApiController]
[Route("/semester-plans/api/read")]
public sealed class SemesterPlanReadApi
{
	[HttpGet(CrudOperationNames.Search)]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> Search([FromQuery] SemesterPlanSchema schema)
	{
		SearchQuery query = new SearchQuery(schema);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}

	[HttpGet("/semester-plans/api/read/by-semester")]
	public async Task<ActionResult<IReadOnlyCollection<SemesterPlanSchema>>> GetBySemester([FromQuery] GetSemesterDisciplines request)
	{
		GetBySemesterQuery query = new GetBySemesterQuery(request.Semester);
		OperationResult<IReadOnlyCollection<SemesterPlan>> result = await query.Handler.Handle(query);
		return result.ToActionResult();
	}
}
