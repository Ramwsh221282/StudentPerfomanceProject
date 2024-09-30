using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Api.Module.Responses.StudentGroups;

public sealed class StudentGroupResponse
{
	public string Name { get; init; }
	public int EntityNumber { get; init; }
	public EducationPlanResponse Plan { get; init; }
	private StudentGroupResponse(StudentGroup group)
	{
		EntityNumber = group.EntityNumber;
		Name = group.Name.Name;
		Plan = EducationPlanResponse.FromEducationPlan(group.EducationPlan);
	}
	public static StudentGroupResponse FromStudentGroup(StudentGroup group) => new StudentGroupResponse(group);
	public static ActionResult<IReadOnlyCollection<StudentGroupResponse>> FromResult(OperationResult<IReadOnlyCollection<StudentGroup>> result) =>
		result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromStudentGroup));
	public static ActionResult<StudentGroupResponse> FromResult(OperationResult<StudentGroup> result) =>
		result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromStudentGroup(result.Result));
}
