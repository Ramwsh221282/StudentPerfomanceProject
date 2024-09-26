using Microsoft.AspNetCore.Mvc;

using StudentPerfomance.Api.Responses.EducationDirections;
using StudentPerfomance.Application;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.EducationPlans;

public sealed class EducationPlanResponse
{
	public uint Year { get; init; }
	public EducationDirectionResponse Direction { get; init; }
	public int EntityNumber { get; init; }
	private EducationPlanResponse(uint year, EducationDirectionResponse direction, int entityNumber)
	{
		Year = year;
		Direction = direction;
		EntityNumber = entityNumber;
	}

	public static EducationPlanResponse FromEducationPlan(EducationPlan plan)
	{
		return new EducationPlanResponse
		(
			plan.Year.Year,
			EducationDirectionResponse.FromEducationDirection(plan.Direction),
			plan.EntityNumber
		);
	}

	public static ActionResult<EducationPlanResponse> FromResult(OperationResult<EducationPlan> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromEducationPlan(result.Result));

	public static ActionResult<IReadOnlyCollection<EducationPlanResponse>> FromResult(OperationResult<IReadOnlyCollection<EducationPlan>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromEducationPlan));
}
