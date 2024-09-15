namespace StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;

public sealed record DepartmentSchema : EntitySchema
{
	public string? Name { get; init; } = string.Empty;
	public DepartmentSchema() { }
	public DepartmentSchema(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
	}
}
