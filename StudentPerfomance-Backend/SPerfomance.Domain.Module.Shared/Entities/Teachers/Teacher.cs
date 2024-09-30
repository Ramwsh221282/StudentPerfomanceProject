using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines;
using SPerfomance.Domain.Module.Shared.Entities.StudentGrades;
using SPerfomance.Domain.Module.Shared.Entities.StudentGrades.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers;

public sealed class Teacher : Entity
{
	private List<Discipline> _disciplines = [];

	private Teacher() : base(Guid.Empty)
	{
		Name = TeacherName.CreateDefault();
		Condition = WorkingCondition.CreateDefault();
		JobTitle = JobTitle.CreateDefault();
	}

	private Teacher(Guid id, TeacherName name, WorkingCondition condition, JobTitle jobTitle, TeachersDepartment department) : base(id)
	{
		Name = name;
		Department = department;
		Condition = condition;
		JobTitle = jobTitle;
	}

	public WorkingCondition Condition { get; private set; } = null!;
	public JobTitle JobTitle { get; private set; } = null!;
	public TeacherName Name { get; private set; }
	public IReadOnlyCollection<Discipline> Disciplines => _disciplines.AsReadOnly();
	public TeachersDepartment Department { get; } = null!;
	public void ChangeName(TeacherName newName) => Name = newName;
	public void ChangeCondition(WorkingCondition condition) => Condition = condition;
	public void ChangeJobTitle(JobTitle jobTitle) => JobTitle = jobTitle;
	public CSharpFunctionalExtensions.Result<StudentGrade> GradeStudent(Student student, Discipline discipline, GradeValue value)
	{
		Discipline? target = _disciplines.FirstOrDefault(d => d.Id == discipline.Id);
		if (target == null)
			return CSharpFunctionalExtensions.Result.Failure<StudentGrade>("Преподаватель не ведёт эту дисциплину");
		CSharpFunctionalExtensions.Result<StudentGrade> request = StudentGrade.Create(this, target, student, value);
		if (request.IsFailure)
			return CSharpFunctionalExtensions.Result.Failure<StudentGrade>(request.Error);
		return request.Value;
	}

	public static CSharpFunctionalExtensions.Result<Teacher> Create
	(
		TeacherName name,
		WorkingCondition condition,
		JobTitle jobTitle,
		TeachersDepartment department
	)
	{
		Teacher teacher = new Teacher(Guid.NewGuid(), name, condition, jobTitle, department);
		return teacher;
	}
}
