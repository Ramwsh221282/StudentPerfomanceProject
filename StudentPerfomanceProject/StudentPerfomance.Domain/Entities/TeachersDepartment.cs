using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.TeacherDepartment;
using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Domain.Entities;

public class TeachersDepartment : Entity
{
	private List<Teacher> _teachers = [];
	private TeachersDepartment() : base(Guid.Empty) { }
	private TeachersDepartment(Guid id, string name) : base(id) => Name = name;
	public string Name { get; private set; }
	public IReadOnlyCollection<Teacher> Teachers => _teachers;
	public int TeachersCount => _teachers.Count;

	public void ChangeDepartmentName(string name)
	{
		Validator<TeachersDepartment> validator = new TeacherDepartmentValidator(name);
		if (!validator.Validate())
			return;
		Name = name;
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

	public static Result<TeachersDepartment> Create(Guid id, string name)
	{
		Validator<TeachersDepartment> validator = new TeacherDepartmentValidator(name);
		if (!validator.Validate())
			return Result.Failure<TeachersDepartment>(validator.GetErrorText());
		return new TeachersDepartment(id, name);
	}
}

public static class TeachersDepartmentExtensions
{
	public static bool IsSameAs(this TeachersDepartment? department, string? departmentName)
	{
		if (department == null || string.IsNullOrWhiteSpace(departmentName)) return false;
		return department.Name == departmentName;
	}
}
