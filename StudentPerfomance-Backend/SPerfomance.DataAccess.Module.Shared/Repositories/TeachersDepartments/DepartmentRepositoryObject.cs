namespace SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;

public sealed class DepartmentRepositoryObject
{
	public string Name { get; private set; } = string.Empty;
	public string ShortName { get; private set; } = string.Empty;

	public DepartmentRepositoryObject WithName(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}

	public DepartmentRepositoryObject WithShortName(string? shortName)
	{
		if (!string.IsNullOrWhiteSpace(shortName)) ShortName = shortName;
		return this;
	}
}
