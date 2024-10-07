using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.EducationPlans.Module.Commands.Create;
using SPerfomance.Application.EducationPlans.Module.Commands.Delete;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Api;

[ApiController]
[Route("/education-plans/api/management")]
public sealed class EducationPlansManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<EducationPlanSchema>> Create([FromBody] EducationPlanSchema plan)
	{
		CreateCommand command = new CreateCommand(plan);
		OperationResult<EducationPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<EducationPlanSchema>> Remove([FromBody] EducationPlanSchema plan)
	{
		DeleteCommand command = new DeleteCommand(plan);
		OperationResult<EducationPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
