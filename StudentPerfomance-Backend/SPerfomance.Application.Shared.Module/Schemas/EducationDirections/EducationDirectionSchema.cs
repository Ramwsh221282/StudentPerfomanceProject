using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationDirections;

public sealed record EducationDirectionSchema : EntitySchema
{
	public int EntityNumber { get; set; } = 0;
	public string Code { get; init; } = string.Empty;
	public string Name { get; init; } = string.Empty;
	public string Type { get; init; } = string.Empty;

	public EducationDirectionSchema() { }

	public EducationDirectionSchema(string? code, string? name, string? type)
	{
		if (!string.IsNullOrWhiteSpace(code)) Code = code;
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(type)) Type = type;
	}

	public EducationDirection CreateDomainObject()
	{
		DirectionCode code = DirectionCode.Create(Code).Value;
		DirectionName name = DirectionName.Create(Name).Value;
		DirectionType type = DirectionType.Create(Type).Value;
		return EducationDirection.Create(code, name, type).Value;
	}

	public DirectionType CreateDirectionType() => DirectionType.Create(Type).Value;
	public DirectionName CreateDirectionName() => DirectionName.Create(Name).Value;
	public DirectionCode CreateDirectionCode() => DirectionCode.Create(Code).Value;
}

public static class EducationDirectionSchemaExtensions
{
	public static EducationDirectionsRepositoryObject ToRepositoryObject(this EducationDirectionSchema schema)
	{
		EducationDirectionsRepositoryObject direction = new EducationDirectionsRepositoryObject()
		.WithDirectionName(schema.Name.CreateValueOrEmpty())
		.WithDirectionType(schema.Type.CreateValueOrEmpty())
		.WithDirectionCode(schema.Code.CreateValueOrEmpty());
		return direction;
	}

	public static EducationDirectionSchema ToSchema(this EducationDirection direction)
	{
		EducationDirectionSchema schema = new EducationDirectionSchema(direction.Code.Code, direction.Name.Name, direction.Type.Type);
		schema.EntityNumber = direction.EntityNumber;
		return schema;
	}

	public static ActionResult<EducationDirectionSchema> ToActionResult(this OperationResult<EducationDirection> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<EducationDirectionSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<EducationDirection>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
