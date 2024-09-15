using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Application;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.SemesterPlans;

public sealed class SemesterPlanResponse
{
	private SemesterPlanResponse
	(
	    int semesterNumber,
	    string groupName,
	    string planName,
	    string disciplineName,
	    string attachedTeacherName,
	    string attachedTeacherSurname,
	    string attachedTeacherThirdName,
	    string attachedTeacherDepartmentName
	)
	{
		SemesterNumber = semesterNumber;
		GroupName = groupName;
		PlanName = planName;
		DisciplineName = disciplineName;
		AttachedTeacherName = attachedTeacherName;
		AttachedTeacherSurname = attachedTeacherSurname;
		AttachedTeacherThirdname = attachedTeacherThirdName;
		AttachedTeacherDepartmentName = attachedTeacherDepartmentName;
	}

	public int SemesterNumber { get; }
	public string GroupName { get; }
	public string PlanName { get; }
	public string DisciplineName { get; }
	public string? AttachedTeacherName { get; }
	public string? AttachedTeacherSurname { get; }
	public string? AttachedTeacherThirdname { get; }
	public string? AttachedTeacherDepartmentName { get; }

	public static SemesterPlanResponse FromSemesterPlan(SemesterPlan plan)
	{
		return new SemesterPlanResponse
		(
		    plan.LinkedSemester.Number.Value,
		    plan.LinkedSemester.Group.Name.Name,
		    plan.PlanName,
		    plan.LinkedDiscipline.Name,
		    plan.LinkedDiscipline.Teacher == null ? null : plan.LinkedDiscipline.Teacher.Name.Name,
		    plan.LinkedDiscipline.Teacher == null ? null : plan.LinkedDiscipline.Teacher.Name.Surname,
		    plan.LinkedDiscipline.Teacher == null ? null : plan.LinkedDiscipline.Teacher.Name.Thirdname,
		    plan.LinkedDiscipline.Teacher == null ? null : plan.LinkedDiscipline.Teacher.Department.Name
		);
	}

	public static ActionResult<SemesterPlanResponse> FromResult(OperationResult<SemesterPlan> result) =>
	result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromSemesterPlan(result.Result));

	public static ActionResult<IReadOnlyCollection<SemesterPlanResponse>> FromResult(OperationResult<IReadOnlyCollection<SemesterPlan>> result) =>
	result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromSemesterPlan));
}
