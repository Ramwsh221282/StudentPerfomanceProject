namespace StudentPerfomance.Application.EntitySchemas.Schemas.Students;

public sealed record StudentSchema : EntitySchema
{
	public string? Name { get; init; } = string.Empty;
	public string? Surname { get; init; } = string.Empty;
	public string? Thirdname { get; init; } = string.Empty;
	public string? State { get; init; } = string.Empty;
	public ulong Recordbook { get; init; } = 0;
	public StudentSchema() { }
	public StudentSchema(string name, string surname, string thirdname, string state, ulong recordbook)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		if (!string.IsNullOrWhiteSpace(state)) State = state;
		if (recordbook > 0) Recordbook = recordbook;
	}
}
