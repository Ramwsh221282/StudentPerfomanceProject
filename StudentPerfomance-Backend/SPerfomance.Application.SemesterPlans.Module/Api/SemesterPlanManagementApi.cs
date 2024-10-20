using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.SemesterPlans.Module.Api.Requests;
using SPerfomance.Application.SemesterPlans.Module.Commands.AttachTeacher;
using SPerfomance.Application.SemesterPlans.Module.Commands.Create;
using SPerfomance.Application.SemesterPlans.Module.Commands.DeattachTeacher;
using SPerfomance.Application.SemesterPlans.Module.Commands.Delete;
using SPerfomance.Application.Shared.Module.DTOs.SemesterPlans;
using SPerfomance.Application.Shared.Module.DTOs.Semesters;
using SPerfomance.Application.Shared.Module.DTOs.Teachers;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Api;

[ApiController]
[Route("/semester-plans/api/management")]
public sealed class SemesterPlanManagementApi : Controller
{
	[HttpPost(CrudOperationNames.Create)]
	public async Task<ActionResult<SemesterPlanSchema>> Create([FromBody] CreateSemesterDiscipline request)
	{
		SemesterSchema semester = request.Semester.ToSchema();
		SemesterPlanSchema plan = request.SemesterPlan.ToSchema();
		string token = request.Token;
		CreateCommand command = new CreateCommand(semester, plan, token);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpDelete(CrudOperationNames.Remove)]
	public async Task<ActionResult<SemesterPlanSchema>> Remove([FromBody] RemoveSemesterDiscipline request)
	{
		SemesterSchema semester = request.Semester.ToSchema();
		SemesterPlanSchema plan = request.SemesterPlan.ToSchema();
		string token = request.Token;
		DeleteCommand command = new DeleteCommand(semester, plan, token);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("/semester-plans/api/management/attach-teacher")]
	public async Task<ActionResult<SemesterPlanSchema>> AttachTeacher([FromBody] TeacherAttachmentRequest request)
	{
		SemesterPlanSchema plan = request.Plan.ToSchema();
		TeacherSchema teacher = request.Teacher.ToSchema();
		string token = request.Token;
		AttachTeacherCommand command = new AttachTeacherCommand(plan, teacher, token);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut("/semester-plans/api/management/deattach-teacher")]
	public async Task<ActionResult<SemesterPlanSchema>> DeattachTeacher([FromBody] TeacherDeattachmentRequest request)
	{
		SemesterPlanSchema plan = request.Plan.ToSchema();
		string token = request.Token;
		DeattachTeacherCommand command = new DeattachTeacherCommand(plan, token);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}

	[HttpPut(CrudOperationNames.Update)]
	public async Task<ActionResult<SemesterPlanSchema>> Update([FromBody] ChangeDisciplineNameRequest request)
	{
		SemesterPlanSchema initial = request.Initial.ToSchema();
		SemesterPlanSchema updated = request.Updated.ToSchema();
		string token = request.Token;
		UpdateCommand command = new UpdateCommand(initial, updated, token);
		OperationResult<SemesterPlan> result = await command.Handler.Handle(command);
		return result.ToActionResult();
	}
}
