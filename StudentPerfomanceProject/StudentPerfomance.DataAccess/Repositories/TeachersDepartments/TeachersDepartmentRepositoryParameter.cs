namespace StudentPerfomance.DataAccess.Repositories.TeachersDepartments;

public sealed class TeachersDepartmentRepositoryParameter
{
	public string? Name { get; private set; }

	public TeachersDepartmentRepositoryParameter WithName(string? name)
	{
		Name = name;
		return this;
	}
}
