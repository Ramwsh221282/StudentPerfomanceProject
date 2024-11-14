using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Models.Semesters.ValueObjects;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Semesters;

public class Semester : AggregateRoot
{
    private readonly List<SemesterPlan> _disciplines = [];

    public SemesterNumber Number { get; init; }

    public EducationPlan Plan { get; private set; }

    public IReadOnlyCollection<SemesterPlan> Disciplines => _disciplines;

    internal Semester()
        : base(Guid.Empty)
    {
        Number = new SemesterNumber();
        Plan = EducationPlan.Empty;
    }

    internal Semester(byte number, EducationPlan plan)
        : base(Guid.NewGuid())
    {
        Number = new SemesterNumber(number);
        Plan = plan;
    }

    internal static Semester Empty => new Semester();

    public Result<SemesterPlan> AddDiscipline(string disciplineName)
    {
        if (_disciplines.Any(d => d.Discipline.Name == disciplineName))
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.DisciplineDublicate());

        Result<SemesterPlan> semesterPlan = SemesterPlan.Create(disciplineName, this);
        if (semesterPlan.IsFailure)
            return Result<SemesterPlan>.Failure(semesterPlan.Error);

        _disciplines.Add(semesterPlan.Value);
        return Result<SemesterPlan>.Success(semesterPlan.Value);
    }

    public Result<SemesterPlan> RemoveDiscipline(SemesterPlan plan)
    {
        if (_disciplines.Any(d => d.Id == plan.Id) == false)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.DoesntBelongSemester());

        _disciplines.Remove(plan);
        return Result<SemesterPlan>.Success(plan);
    }

    public Result<SemesterPlan> FindDiscipline(string disciplineName)
    {
        SemesterPlan? plan = _disciplines.FirstOrDefault(d => d.Discipline.Name == disciplineName);
        return plan == null
            ? Result<SemesterPlan>.Failure(SemesterPlanErrors.NotFound())
            : Result<SemesterPlan>.Success(plan);
    }

    public Result<SemesterPlan> ChangeDisciplineName(SemesterPlan semesterPlan, string newName)
    {
        if (_disciplines.Any(d => d.Discipline.Name == newName))
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.DisciplineDublicate());

        return semesterPlan.ChangeName(newName);
    }

    public Result<SemesterPlan> AttachTeacherToDiscipline(SemesterPlan plan, Teacher teacher)
    {
        if (_disciplines.Any(d => d.Id == plan.Id) == false)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.DoesntBelongSemester());

        return plan.AttachTeacher(teacher);
    }

    public Result<SemesterPlan> DeattachTeacherFromDiscipline(SemesterPlan plan)
    {
        if (_disciplines.Any(d => d.Id == plan.Id) == false)
            return Result<SemesterPlan>.Failure(SemesterPlanErrors.DoesntBelongSemester());

        return plan.DeattachTeacher();
    }
}
