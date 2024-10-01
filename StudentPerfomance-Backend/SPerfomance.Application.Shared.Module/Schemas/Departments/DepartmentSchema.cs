using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.Shared.Module.Schemas.Departments;

public record DepartmentSchema : EntitySchema
{
	public string FullName { get; init; } = string.Empty;
	public string ShortName { get; init; } = string.Empty;

	public DepartmentSchema(string? fullName, string? shortName)
	{
		if (!string.IsNullOrWhiteSpace(fullName)) FullName = fullName;
		if (!string.IsNullOrWhiteSpace(shortName)) ShortName = shortName;
	}

	public TeachersDepartment CreateDomainObject() => TeachersDepartment.Create(FullName).Value;
}
