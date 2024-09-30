namespace SPerfomance.DataAccess.Module.Shared.Repositories.Disciplines;

public sealed class DisciplineRepositoryObject
{
	public string Name { get; private set; } = string.Empty;

	public DisciplineRepositoryObject WithName(string name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}
}
