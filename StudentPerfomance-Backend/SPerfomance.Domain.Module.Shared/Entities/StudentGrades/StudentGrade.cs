using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.Disciplines;
using SPerfomance.Domain.Module.Shared.Entities.StudentGrades.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGrades;

public sealed class StudentGrade : Entity
{
	private StudentGrade() : base(Guid.Empty)
	{
		Value = GradeValue.CreateDefault();
		GradeDate = DateTime.Now;
	}

	private StudentGrade(Guid id, Teacher teacher, Discipline discipline, Student student, GradeValue value) : base(id)
	{
		Teacher = teacher;
		Discipline = discipline;
		Student = student;
		GradeDate = DateTime.Now;
		Value = value;
	}

	public Teacher Teacher { get; } = null!;
	public Discipline Discipline { get; } = null!;
	public Student Student { get; } = null!;
	public DateTime GradeDate { get; }
	public GradeValue Value { get; private set; }
	public void ChangeGrade(GradeValue newValue) => Value = newValue;

	public static CSharpFunctionalExtensions.Result<StudentGrade> Create(Teacher teacher, Discipline discipline, Student student, GradeValue value)
	{
		StudentGrade grade = new StudentGrade(Guid.Empty, teacher, discipline, student, value);
		return grade;
	}
}
