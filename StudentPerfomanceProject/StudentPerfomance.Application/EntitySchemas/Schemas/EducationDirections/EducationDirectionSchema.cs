namespace StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;

public record EducationDirectionSchema : EntitySchema
{
	public string? Code { get; init; } = string.Empty;
	public string? Name { get; init; } = string.Empty;
	public string? Type { get; init; } = string.Empty;

	public EducationDirectionSchema(string? code, string? name, string? type)
	{
		if (!string.IsNullOrWhiteSpace(code)) Code = code;
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(type)) Type = type;
	}
}
