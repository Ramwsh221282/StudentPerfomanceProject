using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Students;

public sealed class StudentsRepositoryObject
{
	public string Name { get; private set; } = string.Empty;
	public string Surname { get; private set; } = string.Empty;
	public string Thirdname { get; private set; } = string.Empty;
	public string State { get; private set; } = string.Empty;
	public ulong Recordbook { get; private set; }
	public StudentGroupsRepositoryObject Group { get; private set; } = new StudentGroupsRepositoryObject();
	public StudentsRepositoryObject WithName(string name)
	{
		if (!string.IsNullOrEmpty(name)) Name = name;
		return this;
	}

	public StudentsRepositoryObject WithSurname(string surname)
	{
		if (!string.IsNullOrEmpty(surname)) Surname = surname;
		return this;
	}

	public StudentsRepositoryObject WithThirdname(string? thirdname)
	{
		if (!string.IsNullOrEmpty(thirdname)) Thirdname = thirdname;
		return this;
	}

	public StudentsRepositoryObject WithState(string state)
	{
		if (!string.IsNullOrEmpty(state)) State = state;
		return this;
	}

	public StudentsRepositoryObject WithRecordbook(ulong recordBook)
	{
		Recordbook = recordBook;
		return this;
	}

	public StudentsRepositoryObject WithGroup(StudentGroupsRepositoryObject group)
	{
		Group = group;
		return this;
	}
}
