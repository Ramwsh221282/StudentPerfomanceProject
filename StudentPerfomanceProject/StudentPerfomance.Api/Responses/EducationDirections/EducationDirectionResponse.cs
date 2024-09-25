using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Application;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Api.Responses.EducationDirections;

public sealed class EducationDirectionResponse
{
	public string Name { get; init; }
	public string Code { get; init; }
	public string Type { get; init; }
	public int EntityNumber { get; init; }
	private EducationDirectionResponse(string name, string code, string type, int entityNumber)
	{
		Name = name;
		Code = code;
		Type = type;
		EntityNumber = entityNumber;
	}

	public static EducationDirectionResponse FromEducationDirection(EducationDirection direction)
	{
		return new EducationDirectionResponse
		(
			direction.Name.Name,
			direction.Code.Code,
			direction.Type.Type,
			direction.EntityNumber
		);
	}

	public static ActionResult<EducationDirectionResponse> FromResult(OperationResult<EducationDirection> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(FromEducationDirection(result.Result));

	public static ActionResult<IReadOnlyCollection<EducationDirectionResponse>> FromResult(OperationResult<IReadOnlyCollection<EducationDirection>> result) =>
		result.IsFailed ? new BadRequestObjectResult(result.Error) : new OkObjectResult(result.Result.Select(FromEducationDirection));
}
