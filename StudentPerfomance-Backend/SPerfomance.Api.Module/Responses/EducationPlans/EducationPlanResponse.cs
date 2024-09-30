using Microsoft.AspNetCore.Mvc;

using SPerfomance.Api.Module.Responses.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Api.Module.Responses.EducationPlans;

public sealed class EducationPlanResponse
{
	public uint Year { get; init; }
	public EducationDirectionResponse Direction { get; init; }
	public int EntityNumber { get; init; }
	private EducationPlanResponse(EducationPlan plan)
	{
		Year = plan.Year.Year;
		Direction = EducationDirectionResponse.FromEducationDirection(plan.Direction);
		EntityNumber = plan.EntityNumber;
	}

	public static EducationPlanResponse FromEducationPlan(EducationPlan plan) => new EducationPlanResponse(plan);

	public static ActionResult<EducationPlanResponse> FromResult(OperationResult<EducationPlan> result) =>
		result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromEducationPlan(result.Result));

	public static ActionResult<IReadOnlyCollection<EducationPlanResponse>> FromResult(OperationResult<IReadOnlyCollection<EducationPlan>> result) =>
		result.Result == null || result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromEducationPlan));
}
