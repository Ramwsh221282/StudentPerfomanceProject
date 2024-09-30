using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationDirections;

public sealed record EducationDirectionSchema : EntitySchema
{
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

	public DirectionName CreateDirectionName() => DirectionName.Create(Name).Value;
	public DirectionCode CreateDirectionCode() => DirectionCode.Create(Code).Value;
}
