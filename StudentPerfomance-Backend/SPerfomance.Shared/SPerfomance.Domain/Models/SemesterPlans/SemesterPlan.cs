using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Models.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.SemesterPlans;

public class SemesterPlan : DomainEntity
{
	public DisciplineName Discipline { get; private set; }

	public Semester Semester { get; private set; }

	public Teacher? Teacher { get; private set; }

	internal SemesterPlan() : base(Guid.Empty)
	{
		Discipline = DisciplineName.Empty;
		Semester = Semester.Empty;
	}

	internal SemesterPlan(DisciplineName name, Semester semester) : base(Guid.NewGuid())
	{
		Discipline = name;
		Semester = semester;
	}

	internal static SemesterPlan Empty => new SemesterPlan();

	internal Result<SemesterPlan> AttachTeacher(Teacher teacher)
	{
		if (teacher != null)
			return Result<SemesterPlan>.Failure(SemesterPlanErrors.TeacherAlreadyAttacher());

		Teacher = teacher;
		return Result<SemesterPlan>.Success(this);
	}

	internal Result<SemesterPlan> ChangeName(string name)
	{
		Result<DisciplineName> newName = DisciplineName.Create(name);
		if (newName.IsFailure)
			return Result<SemesterPlan>.Failure(newName.Error);

		if (Discipline == newName.Value)
			return Result<SemesterPlan>.Success(this);

		Discipline = newName.Value;
		return Result<SemesterPlan>.Success(this);
	}

	internal Result<SemesterPlan> DeattachTeacher()
	{
		if (Teacher == null)
			return Result<SemesterPlan>.Success(this);

		return Result<SemesterPlan>.Success(this);
	}

	internal static Result<SemesterPlan> Create(string disciplineName, Semester semester)
	{
		Result<DisciplineName> name = DisciplineName.Create(disciplineName);
		if (name.IsFailure)
			return Result<SemesterPlan>.Failure(name.Error);

		return Result<SemesterPlan>.Success(new SemesterPlan(name.Value, semester));
	}
}
