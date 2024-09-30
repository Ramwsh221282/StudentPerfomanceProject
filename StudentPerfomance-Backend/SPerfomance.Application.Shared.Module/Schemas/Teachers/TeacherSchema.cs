using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;

namespace SPerfomance.Application.Shared.Module.Schemas.Teachers;

public sealed record TeacherSchema : EntitySchema
{
	public string Name { get; init; } = string.Empty;
	public string Surname { get; init; } = string.Empty;
	public string Thirdname { get; init; } = string.Empty;
	public string WorkingCondition { get; init; } = string.Empty;
	public string JobTitle { get; init; } = string.Empty;
	public TeacherSchema() { }
	public TeacherSchema(string? name, string? surname, string? thirdname, string? workingCondition, string? jobTitle)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		if (!string.IsNullOrWhiteSpace(workingCondition)) WorkingCondition = workingCondition;
		if (!string.IsNullOrWhiteSpace(jobTitle)) JobTitle = jobTitle;
	}
}
