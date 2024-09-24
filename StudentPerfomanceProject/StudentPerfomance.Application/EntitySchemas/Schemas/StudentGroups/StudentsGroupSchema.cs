using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

public sealed record StudentsGroupSchema : EntitySchema
{
	public Result<GroupName> Name { get; private set; }
	public StudentsGroupSchema() { }

	public StudentsGroupSchema(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = GroupName.Create(name);
	}
}
