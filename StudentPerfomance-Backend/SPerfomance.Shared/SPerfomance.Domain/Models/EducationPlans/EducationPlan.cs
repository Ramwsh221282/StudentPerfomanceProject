using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.EducationPlans.ValueObjects;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Semesters.Errors;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationPlans;

public class EducationPlan : AggregateRoot
{
    private List<StudentGroup> _groups = [];

    private List<Semester> _semesters = [];

    public PlanYear Year { get; private set; }

    public EducationDirection Direction { get; private set; }

    public IReadOnlyCollection<StudentGroup> Groups => _groups;

    public IReadOnlyCollection<Semester> Semesters => _semesters;

    private EducationPlan()
        : base(Guid.Empty)
    {
        Year = PlanYear.Default;
        Direction = EducationDirection.Empty;
    }

    private EducationPlan(PlanYear year, EducationDirection direction)
        : base(Guid.NewGuid())
    {
        Year = year;
        Direction = direction;
        CreateSemesters();
    }

    internal static EducationPlan Empty => new EducationPlan();

    internal static Result<EducationPlan> Create(int year, EducationDirection direction)
    {
        Result<PlanYear> yearCreation = PlanYear.Create(year);
        if (yearCreation.IsFailure)
            return Result<EducationPlan>.Failure(yearCreation.Error);

        return Result<EducationPlan>.Success(new EducationPlan(yearCreation.Value, direction));
    }

    public Result<StudentGroup> AddStudentGroup(StudentGroup group, byte activeSemesterNumber)
    {
        if (_groups.Any(g => g.Name == group.Name) == true)
            return Result<StudentGroup>.Failure(
                StudentGroupErrors.EducationPlanHasGroupAlreadyError(this, group)
            );

        Result<StudentGroup> attachment = group.SetEducationPlan(this, activeSemesterNumber);
        if (attachment.IsFailure)
            return attachment;

        _groups.Add(group);
        return Result<StudentGroup>.Success(group);
    }

    public Result<StudentGroup> RemoveStudentGroup(StudentGroup group)
    {
        if (_groups.Any(g => g.Name == group.Name) == false)
            return Result<StudentGroup>.Failure(
                StudentGroupErrors.StudentGroupDoesntBelongEducationPlan(this, group)
            );

        group.DeattachEducationPlan();
        _groups.Remove(group);
        return Result<StudentGroup>.Success(group);
    }

    public Result<Semester> FindSemester(byte number)
    {
        Semester? semester = _semesters.FirstOrDefault(s => s.Number.Number == number);
        return semester == null
            ? Result<Semester>.Failure(SemesterErrors.NotFound())
            : Result<Semester>.Success(semester);
    }

    public Result<EducationPlan> ChangeYear(int year)
    {
        Result<PlanYear> newYear = PlanYear.Create(year);
        if (newYear.IsFailure)
            return Result<EducationPlan>.Failure(newYear.Error);

        if (Year == newYear.Value)
            return Result<EducationPlan>.Success(this);

        Year = newYear.Value;
        return Result<EducationPlan>.Success(this);
    }

    private void CreateSemesters()
    {
        if (Direction.Type == DirectionType.Bachelor)
        {
            for (int index = 1; index <= 8; index++)
            {
                Semester semester = new Semester((byte)index, this);
                _semesters.Add(semester);
            }
        }

        if (Direction.Type == DirectionType.Magister)
        {
            for (int index = 1; index <= 4; index++)
            {
                Semester semester = new Semester((byte)index, this);
                _semesters.Add(semester);
            }
        }
    }
}
