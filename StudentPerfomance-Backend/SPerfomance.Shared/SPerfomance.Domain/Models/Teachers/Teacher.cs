using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.Teachers.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Teachers;

public class Teacher : DomainEntity
{
    private readonly List<SemesterPlan> _disciplines = [];

    public TeacherName Name { get; private set; }

    public TeacherJobTitle JobTitle { get; private set; }

    public TeacherWorkingState State { get; private set; }

    public TeachersDepartments Department { get; init; }

    public IReadOnlyCollection<SemesterPlan> Disciplines => _disciplines;

    internal Teacher()
        : base(Guid.Empty)
    {
        Name = TeacherName.Empty;
        JobTitle = TeacherJobTitle.Empty;
        State = TeacherWorkingState.Empty;
        Department = TeachersDepartments.Empty;
    }

    internal Teacher(
        TeacherName name,
        TeacherWorkingState state,
        TeacherJobTitle jobTitle,
        TeachersDepartments department
    )
        : base(Guid.NewGuid())
    {
        Name = name;
        State = state;
        JobTitle = jobTitle;
        Department = department;
    }

    internal static Teacher Empty => new Teacher();

    public Result<Teacher> ChangeJobTitle(string title)
    {
        Result<TeacherJobTitle> newJob = TeacherJobTitle.Create(title);
        if (newJob.IsFailure)
            return Result<Teacher>.Failure(newJob.Error);

        if (JobTitle == newJob.Value)
            return Result<Teacher>.Success(this);

        JobTitle = newJob.Value;
        return Result<Teacher>.Success(this);
    }

    public Result<Teacher> ChangeName(string name, string surname, string? patronymic)
    {
        Result<TeacherName> newName = TeacherName.Create(name, surname, patronymic);
        if (newName.IsFailure)
            return Result<Teacher>.Failure(newName.Error);

        if (newName.Value == Name)
            return Result<Teacher>.Success(this);

        Name = newName.Value;
        return Result<Teacher>.Success(this);
    }

    public Result<Teacher> ChangeState(string state)
    {
        Result<TeacherWorkingState> newState = TeacherWorkingState.Create(state);
        if (newState.IsFailure)
            return Result<Teacher>.Failure(newState.Error);

        if (newState.Value == State)
            return Result<Teacher>.Success(this);

        State = newState.Value;
        return Result<Teacher>.Success(this);
    }

    public Result<SemesterPlan> AttachDiscipline(SemesterPlan plan)
    {
        if (_disciplines.Any(d => d.Discipline == plan.Discipline))
            return Result<SemesterPlan>.Failure(
                SemesterPlanErrors.TeacherHasDisciplineAlready(this, plan)
            );

        plan.AttachTeacher(this);
        _disciplines.Add(plan);
        return Result<SemesterPlan>.Success(plan);
    }

    public Result<SemesterPlan> DeattachDiscipline(SemesterPlan plan)
    {
        if (_disciplines.Any(d => d.Id == plan.Id) == false)
            return Result<SemesterPlan>.Failure(
                SemesterPlanErrors.TeacherDoesntChargeDiscipline(this)
            );

        _disciplines.Remove(plan);
        plan.DeattachTeacher();
        return Result<SemesterPlan>.Success(plan);
    }
}
