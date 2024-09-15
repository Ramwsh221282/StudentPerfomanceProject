namespace StudentPerfomance.DataAccess.Repositories.Disciplines;

public sealed class DisciplineRepositoryParameter
{
	public string? Name { get; private set; }

	public DisciplineRepositoryParameter WithName(string? name)
	{
		Name = name;
		return this;
	}
}
