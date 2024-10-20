using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.DTOs.EducationPlans;
using SPerfomance.Application.Shared.Module.DTOs.StudentGroups;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.StudentGroups.Module.Api.Requests;
using SPerfomance.Application.StudentGroups.Module.Commands.AttachEducationPlan;
using SPerfomance.Application.StudentGroups.Module.Commands.Create;
using SPerfomance.Application.StudentGroups.Module.Commands.DeattachEducationPlan;
using SPerfomance.Application.StudentGroups.Module.Commands.Delete;
using SPerfomance.Application.StudentGroups.Module.Commands.MergeGroups;
using SPerfomance.Application.StudentGroups.Module.Commands.Update;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Application.StudentGroups.Module.Api;

[ApiController]
[Route("/student-groups/api/management")]
public sealed class StudentGroupsManagementApi : ControllerBase
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<StudentsGroupSchema>> Create([FromBody] StudentGroupCreateRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		string token = request.Token;
		CreateCommand command = new CreateCommand(group, token);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<StudentsGroupSchema>> Remove([FromBody] StudentGroupRemoveRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		string token = request.Token;
		DeleteCommand command = new DeleteCommand(group, token);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<StudentsGroupSchema>> Update([FromBody] StudentGroupUpdateRequest request)
	{
		StudentsGroupSchema initial = request.Initial.ToSchema();
		StudentsGroupSchema updated = request.Updated.ToSchema();
		string token = request.Token;
		UpdateCommand command = new UpdateCommand(initial, updated, token);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("plan-attachment")]
	public async Task<ActionResult<StudentsGroupSchema>> AttachPlan([FromBody] StudentGroupPlanAttachmentRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		EducationPlanSchema plan = request.Plan.ToSchema();
		string token = request.Token;
		AttachEducationPlanCommand command = new AttachEducationPlanCommand(group, plan, token);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("plan-deattachment")]
	public async Task<ActionResult<StudentsGroupSchema>> DeattachPlan([FromBody] DeattachEducationPlanRequest request)
	{
		StudentsGroupSchema group = request.Group.ToSchema();
		string token = request.Token;
		DeattachEducationPlanCommand command = new DeattachEducationPlanCommand(group, token);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("group-merge")]
	public async Task<ActionResult<StudentsGroupSchema>> Merge([FromBody] StudentGroupMergeRequest request)
	{
		if (request.initial == null || request.target == null)
			return new BadRequestObjectResult(new GroupNotFoundError().ToString());

		StudentsGroupSchema initial = request.initial.ToSchema();
		StudentsGroupSchema target = request.target.ToSchema();
		string token = request.Token;
		MergeGroupCommand command = new MergeGroupCommand(initial, target, token);
		OperationResult<StudentGroup> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
