namespace StudentPerfomance.DataAccess.Repositories.StudentGroups;

public sealed class StudentGroupsRepositoryParameter
{
	public string? Name { get; private set; }
	public StudentGroupsRepositoryParameter WithName(string name)
	{
		Name = name;
		return this;
	}
}
