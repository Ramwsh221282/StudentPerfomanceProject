using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;

public sealed class TeacherRepositoryObject
{
	public string Name { get; private set; } = string.Empty;
	public string Surname { get; private set; } = string.Empty;
	public string Thirdname { get; private set; } = string.Empty;
	public string JobTitle { get; private set; } = string.Empty;
	public string WorkingCondition { get; private set; } = string.Empty;
	public DepartmentRepositoryObject Department { get; private set; } = new DepartmentRepositoryObject();

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

	public TeacherRepositoryObject WithJobTitle(string jobtitle)
	{
		if (!string.IsNullOrWhiteSpace(jobtitle)) JobTitle = jobtitle;
		return this;
	}

	public TeacherRepositoryObject WithWorkingCondition(string condition)
	{
		if (!string.IsNullOrWhiteSpace(condition)) WorkingCondition = condition;
		return this;
	}

	public TeacherRepositoryObject WithDepartment(DepartmentRepositoryObject department)
	{
		Department = department;
		return this;
	}
}
