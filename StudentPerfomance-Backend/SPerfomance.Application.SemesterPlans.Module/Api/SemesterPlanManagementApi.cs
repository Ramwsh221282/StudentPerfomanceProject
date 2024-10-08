using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.SemesterPlans.Module.Commands.AttachTeacher;
using SPerfomance.Application.SemesterPlans.Module.Commands.Create;
using SPerfomance.Application.SemesterPlans.Module.Commands.DeattachTeacher;
using SPerfomance.Application.SemesterPlans.Module.Commands.Delete;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Api;

[ApiController]
[Route("/semester-plans/api/management")]
public sealed class SemesterPlanManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<SemesterPlanSchema>> Create([FromBody] SemesterPlanSchema plan)
	{
		CreateCommand command = new CreateCommand(plan);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<SemesterPlanSchema>> Remove([FromBody] SemesterPlanSchema plan)
	{
		DeleteCommand command = new DeleteCommand(plan);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("/attach-teacher")]
	public async Task<ActionResult<SemesterPlanSchema>> AttachTeacher([FromQuery] SemesterPlanSchema plan, [FromQuery] TeacherSchema teacher)
	{
		AttachTeacherCommand command = new AttachTeacherCommand(plan, teacher);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("/deattach-teacher")]
	public async Task<ActionResult<SemesterPlanSchema>> DeattachTeacher([FromQuery] SemesterPlanSchema plan)
	{
		DeattachTeacherCommand command = new DeattachTeacherCommand(plan);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<SemesterPlanSchema>> Update([FromQuery] SemesterPlanSchema oldSchema, [FromQuery] SemesterPlanSchema newSchema)
	{
		UpdateCommand command = new UpdateCommand(oldSchema, newSchema);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
