using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Validators;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

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
		FullName = name.FormatSpaces().FirstCharacterToUpper();
		ShortName = ConstructShortName(name);
	}

	public string ShortName { get; private set; }
	public string FullName { get; private set; }
	public IReadOnlyCollection<Teacher> Teachers => _teachers;

	public CSharpFunctionalExtensions.Result ChangeDepartmentName(string name)
	{
		Validator<string> validator = new DepartmentNameValidator(name);
		if (!validator.Validate())
			return Failure(validator.GetErrorText());

		FullName = name;
		ShortName = ConstructShortName(name);
		return Success();
	}

	public CSharpFunctionalExtensions.Result AddTeacher(Teacher? teacher)
	{
		if (teacher == null)
			return Failure(new TeacherNotFoundError().ToString());

		if (_teachers.Any(t => t.Name == teacher.Name &&
		t.JobTitle == teacher.JobTitle &&
		t.Condition == teacher.Condition))
			return Failure(new DepartmentTeacherDublicateError().ToString());

		_teachers.Add(teacher);
		return Success();
	}

	public CSharpFunctionalExtensions.Result RemoveTeacher(Teacher teacher)
	{
		Teacher? target = _teachers.FirstOrDefault(t => t.Id == teacher.Id);
		if (target == null)
			return Failure(new TeacherNotFoundError().ToString());

		_teachers.Remove(target);
		return Success();
	}

	public static CSharpFunctionalExtensions.Result<TeachersDepartment> Create(string name)
	{
		TeachersDepartment department = new TeachersDepartment(Guid.NewGuid(), name);
		Validator<TeachersDepartment> validator = new TeacherDepartmentValidator(department);
		return validator.Validate() ? department : CSharpFunctionalExtensions.Result.Failure<TeachersDepartment>(validator.GetErrorText());
	}

	private string ConstructShortName(string fullname)
	{
		string shortName = fullname.CreateAcronymus();
		return shortName;
	}

	public override string ToString() => FullName;
}
