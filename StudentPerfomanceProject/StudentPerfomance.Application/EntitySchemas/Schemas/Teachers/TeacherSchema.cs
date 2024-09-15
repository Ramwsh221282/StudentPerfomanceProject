namespace StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;

public sealed record TeacherSchema : EntitySchema
{
	public string? Name { get; init; } = string.Empty;
	public string? Surname { get; init; } = string.Empty;
	public string? Thirdname { get; init; } = string.Empty;
	public TeacherSchema() { }
	public TeacherSchema(string? name, string? surname, string? thirdname)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
	}
}
