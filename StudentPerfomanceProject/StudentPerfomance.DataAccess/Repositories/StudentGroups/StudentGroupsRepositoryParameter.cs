using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.ValueObjects.StudentGroup;

namespace StudentPerfomance.DataAccess.Repositories.StudentGroups;

public sealed class StudentGroupsRepositoryParameter
{
	public string? Name { get; private set; }
	public StudentGroupsRepositoryParameter WithName(Result<GroupName> name)
	{
		Name = name.IsFailure switch
		{
			true => string.Empty,
			false => name.Value.Name,
		};
		return this;
	}
}
