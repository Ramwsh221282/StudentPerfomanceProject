namespace SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;

public sealed class DepartmentRepositoryObject
{
	public string Name { get; private set; } = string.Empty;
	public DepartmentRepositoryObject WithName(string? name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}
}
