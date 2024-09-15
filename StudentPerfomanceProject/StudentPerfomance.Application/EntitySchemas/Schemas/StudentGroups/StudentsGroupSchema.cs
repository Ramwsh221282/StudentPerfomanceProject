namespace StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

public sealed record StudentsGroupSchema : EntitySchema
{
	public string? Name { get; init; } = string.Empty;

	public StudentsGroupSchema() { }

	public StudentsGroupSchema(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
	}
}
