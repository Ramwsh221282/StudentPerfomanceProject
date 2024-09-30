namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;

public sealed class TeacherRepositoryObject
{
	public string Name { get; private set; } = string.Empty;
	public string Surname { get; private set; } = string.Empty;
	public string Thirdname { get; private set; } = string.Empty;

	public TeacherRepositoryObject WithName(string name)
	{
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		return this;
	}
	public TeacherRepositoryObject WithSurname(string surname)
	{
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		return this;
	}
	public TeacherRepositoryObject WithThirdname(string thirdname)
	{
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
		return this;
	}
}
