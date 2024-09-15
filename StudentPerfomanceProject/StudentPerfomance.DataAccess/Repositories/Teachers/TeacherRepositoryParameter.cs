namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public sealed class TeacherRepositoryParameter
{
	public string? Name { get; private set; }
	public string? Surname { get; private set; }
	public string? Thirdname { get; private set; }

	public TeacherRepositoryParameter WithName(string? name)
	{
		Name = name;
		return this;
	}
	public TeacherRepositoryParameter WithSurname(string? surname)
	{
		Surname = surname;
		return this;
	}
	public TeacherRepositoryParameter WithThirdname(string? thirdname)
	{
		Thirdname = thirdname;
		return this;
	}
}
