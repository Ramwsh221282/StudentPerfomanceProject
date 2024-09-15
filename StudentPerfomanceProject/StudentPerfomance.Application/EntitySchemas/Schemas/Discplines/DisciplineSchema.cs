namespace StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;

public record DisciplineSchema : EntitySchema
{
	public string? Name { get; init; } = string.Empty;

	public DisciplineSchema() { }

	public DisciplineSchema(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
	}
}
