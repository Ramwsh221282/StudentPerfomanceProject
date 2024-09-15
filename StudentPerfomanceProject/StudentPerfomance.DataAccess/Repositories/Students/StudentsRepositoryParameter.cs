namespace StudentPerfomance.DataAccess.Repositories.Students;

public sealed class StudentsRepositoryParameter
{
	public string? Name { get; private set; }
	public string? Surname { get; private set; }
	public string? Thirdname { get; private set; }
	public string? State { get; private set; }
	public ulong Recordbook { get; private set; }
	public string? GroupName { get; private set; }

	public StudentsRepositoryParameter WithName(string name)
	{
		Name = name;
		return this;
	}

	public StudentsRepositoryParameter WithSurname(string surname)
	{
		Surname = surname;
		return this;
	}

	public StudentsRepositoryParameter WithThirdname(string? thirdname)
	{
		Thirdname = thirdname;
		return this;
	}

	public StudentsRepositoryParameter WithState(string state)
	{
		State = state;
		return this;
	}

	public StudentsRepositoryParameter WithRecordbook(ulong recordBook)
	{
		Recordbook = recordBook;
		return this;
	}

	public StudentsRepositoryParameter WithGroupName(string groupName)
	{
		GroupName = groupName;
		return this;
	}
}
