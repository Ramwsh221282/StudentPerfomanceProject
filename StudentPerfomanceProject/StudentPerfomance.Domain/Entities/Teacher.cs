using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Teacher;
using StudentPerfomance.Domain.ValueObjects.StudentGrade;
using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Domain.Entities;

public class Teacher : Entity
{
	private List<Discipline> _disciplines = [];

	private Teacher() : base(Guid.Empty) { }

	private Teacher(Guid id, TeacherName name, TeachersDepartment department) : base(id)
	{
		Name = name;
		Department = department;
	}

	public TeacherName Name { get; private set; }

	public IReadOnlyCollection<Discipline> Disciplines
	{
		get => _disciplines.AsReadOnly();
	}

	public TeachersDepartment Department { get; } = null!;

	public void ChangeName(TeacherName newName) => Name = newName;

	public Result<StudentGrade> GradeStudent(Student student, Discipline discipline, GradeValue value)
	{
		Discipline? target = _disciplines.FirstOrDefault(d => d.Id == discipline.Id);
		if (target == null)
			return Result.Failure<StudentGrade>("Преподаватель не ведёт эту дисциплину");
		Result<StudentGrade> request = StudentGrade.Create(Guid.NewGuid(), this, target, student, value);
		if (request.IsFailure)
			return Result.Failure<StudentGrade>(request.Error);
		return request.Value;
	}

	public static Result<Teacher> Create(Guid id, TeacherName name, TeachersDepartment department)
	{
		Teacher teacher = new Teacher(id, name, department);
		Validator<Teacher> validator = new TeacherValidator(teacher);
		if (!validator.Validate())
			return Result.Failure<Teacher>(validator.GetErrorText());
		return teacher;
	}
}
