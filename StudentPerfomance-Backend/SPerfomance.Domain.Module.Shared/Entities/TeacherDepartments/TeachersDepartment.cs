using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Validators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

public sealed class TeachersDepartment : Entity
{
	private List<Teacher> _teachers = [];
	private TeachersDepartment() : base(Guid.Empty)
	{
		FullName = string.Empty;
		ShortName = string.Empty;
	}
	private TeachersDepartment(Guid id, string name) : base(id)
	{
		FullName = name;
		ShortName = ConstructShortName(name);
	}
	public string ShortName { get; private set; }
	public string FullName { get; private set; }
	public IReadOnlyCollection<Teacher> Teachers => _teachers;
	public void ChangeDepartmentName(string name)
	{
		FullName = name;
		ShortName = ConstructShortName(name);
	}
	public Teacher? TryFindTeacher(TeacherName name) =>
		_teachers.FirstOrDefault
		(
			t => t.Name.Name == name.Name &&
			t.Name.Surname == name.Surname &&
			t.Name.Thirdname == name.Thirdname
		);

	public void AddTeacher(Teacher teacher) => _teachers.Add(teacher);

	public void RemoveTeacher(Teacher teacher)
	{
		Teacher? target = _teachers.FirstOrDefault(t => t.Id == teacher.Id);
		if (target != null)
			_teachers.Remove(target);
	}

	public static CSharpFunctionalExtensions.Result<TeachersDepartment> Create(string name)
	{
		TeachersDepartment department = new TeachersDepartment(Guid.NewGuid(), name);
		Validator<TeachersDepartment> validator = new TeacherDepartmentValidator(department);
		return validator.Validate() ? department : CSharpFunctionalExtensions.Result.Failure<TeachersDepartment>(validator.GetErrorText());
	}

	private string ConstructShortName(string fullname)
	{
		StringBuilder nameBuilder = new StringBuilder();
		string[] nameParts = fullname.Split(' ');
		foreach (var part in nameParts)
		{
			nameBuilder.Append(part[0]);
		}
		return nameBuilder.ToString();
	}

	public override string ToString() => FullName;
}
