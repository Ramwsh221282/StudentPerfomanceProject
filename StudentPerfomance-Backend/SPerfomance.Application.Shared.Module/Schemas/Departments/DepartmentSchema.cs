using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.Shared.Module.Schemas.Departments;

public record DepartmentSchema : EntitySchema
{
	public string FullName { get; init; } = string.Empty;
	public DepartmentSchema() { }
	public DepartmentSchema(string? fullName)
	{
		if (!string.IsNullOrWhiteSpace(fullName)) FullName = fullName;
	}

	public TeachersDepartment CreateDomainObject() => TeachersDepartment.Create(FullName).Value;
}
