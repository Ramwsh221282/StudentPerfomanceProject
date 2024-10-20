using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.EducationPlans.Module.Api.Requests;
using SPerfomance.Application.EducationPlans.Module.Commands.Create;
using SPerfomance.Application.EducationPlans.Module.Commands.Delete;
using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api;

[ApiController]
[Route("/education-plans/api/management")]
public sealed class EducationPlansManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<EducationPlanSchema>> Create([FromBody] EducationPlanActionRequest request)
	{
		EducationPlanDTO plan = request.Plan;
		string token = request.Token;
		CreateCommand command = new CreateCommand(plan, token);
		OperationResult<EducationPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<EducationPlanSchema>> Remove([FromBody] EducationPlanActionRequest request)
	{
		EducationPlanDTO plan = request.Plan;
		string token = request.Token;
		DeleteCommand command = new DeleteCommand(plan, token);
		OperationResult<EducationPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
