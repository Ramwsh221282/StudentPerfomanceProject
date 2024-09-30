using Microsoft.AspNetCore.Mvc;

using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Api.Module.Responses.EducationDirections;

public class EducationDirectionResponse
{
	public string Name { get; init; }
	public string Code { get; init; }
	public string Type { get; init; }
	public int EntityNumber { get; init; }
	private EducationDirectionResponse(EducationDirection direction)
	{
		Name = direction.Name.Name;
		Code = direction.Code.Code;
		Type = direction.Type.Type;
		EntityNumber = direction.EntityNumber;
	}

	public static EducationDirectionResponse FromEducationDirection(EducationDirection direction) => new EducationDirectionResponse(direction);
	public static ActionResult<EducationDirectionResponse> FromResult(OperationResult<EducationDirection> result) =>
		result.IsFailed || result.Result == null
		? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromEducationDirection(result.Result));
	public static ActionResult<IReadOnlyCollection<EducationDirectionResponse>> FromResult(OperationResult<IReadOnlyCollection<EducationDirection>> result) =>
		result.IsFailed || result.Result == null
		? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromEducationDirection));
}
