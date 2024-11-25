using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.ValueObjects;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.Semesters;
using SPerfomance.Domain.Models.Semesters.Errors;
using SPerfomance.Domain.Models.StudentGroups.Errors;
using SPerfomance.Domain.Models.StudentGroups.ValueObjects;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Models.Students.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.StudentGroups;

public class StudentGroup : DomainEntity
{
    private readonly List<Student> _students = [];

    private List<AssignmentWeek> _weeks = [];

    public StudentGroupName Name { get; private set; }

    public EducationPlan? EducationPlan { get; private set; }

    public Semester? ActiveGroupSemester { get; private set; }

    public IReadOnlyCollection<Student> Students => _students;

    public IReadOnlyCollection<AssignmentWeek> Weeks => _weeks;

    private StudentGroup()
        : base(Guid.Empty)
    {
        Name = StudentGroupName.Empty;
    }

    private StudentGroup(StudentGroupName name)
        : base(Guid.NewGuid())
    {
        Name = name;
    }

    internal static StudentGroup Empty => new StudentGroup();

    public static Result<StudentGroup> Create(string name)
    {
        Result<StudentGroupName> nameCreation = StudentGroupName.Create(name);
        return nameCreation.IsFailure
            ? Result<StudentGroup>.Failure(nameCreation.Error)
            : Result<StudentGroup>.Success(new StudentGroup(nameCreation.Value));
    }

    internal Result<StudentGroup> SetEducationPlan(EducationPlan plan, byte activeNumber)
    {
        Semester? active = plan.Semesters.FirstOrDefault(s => s.Number.Number == activeNumber);
        if (active == null)
            return SemesterErrors.NotFound();

        ActiveGroupSemester = active;
        EducationPlan = plan;
        return this;
    }

    public void SetActiveGroupSemester(Semester semester)
    {
        ActiveGroupSemester = semester;
    }

    public void SetNextSemester()
    {
        if (EducationPlan is null)
            return;

        if (ActiveGroupSemester is null)
            return;

        if (EducationPlan.Direction.Type == DirectionType.Bachelor)
        {
            byte finalSemesterValue = 8;
            if (ActiveGroupSemester.Number.Number == finalSemesterValue)
                return;
            byte nextSemesterValue = (byte)(ActiveGroupSemester.Number.Number + 1);
            var nextSemester = EducationPlan.Semesters.FirstOrDefault(s =>
                s.Number.Number == nextSemesterValue
            )!;
            ActiveGroupSemester = nextSemester;
        }

        if (EducationPlan.Direction.Type == DirectionType.Magister)
        {
            byte finalSemesterValue = 4;
            if (ActiveGroupSemester.Number.Number == finalSemesterValue)
                return;
            byte nextSemesterValue = (byte)(ActiveGroupSemester.Number.Number + 1);
            var nextSemester = EducationPlan.Semesters.FirstOrDefault(s =>
                s.Number.Number == nextSemesterValue
            )!;
            ActiveGroupSemester = nextSemester;
        }
    }

    internal void DeattachEducationPlan()
    {
        if (EducationPlan == null)
            return;

        EducationPlan = null;
    }

    public Result<StudentGroup> ChangeName(string name)
    {
        Result<StudentGroupName> nameCreation = StudentGroupName.Create(name);
        if (nameCreation.IsFailure)
            return Result<StudentGroup>.Failure(nameCreation.Error);

        Name = nameCreation.Value;
        return Result<StudentGroup>.Success(this);
    }

    public Result<Student> AddStudent(
        string name,
        string surname,
        string? patronymic,
        ulong recordbook,
        string state
    )
    {
        if (
            _students.Any(s =>
                s.Name.Name == name
                && s.Name.Surname == surname
                && s.Name.Patronymic == patronymic
                && s.Recordbook.Recordbook == recordbook
                && s.State.State == state
            )
        )
            return Result<Student>.Failure(StudentErrors.StudentDublicateError(this));

        Result<StudentName> nameCreation = StudentName.Create(name, surname, patronymic);
        if (nameCreation.IsFailure)
            return Result<Student>.Failure(nameCreation.Error);

        Result<StudentRecordbook> recordBookCreation = StudentRecordbook.Create(recordbook);
        if (recordBookCreation.IsFailure)
            return Result<Student>.Failure(recordBookCreation.Error);

        Result<StudentState> stateCreation = StudentState.Create(state);
        if (stateCreation.IsFailure)
            return Result<Student>.Failure(stateCreation.Error);

        Student student = new Student(
            nameCreation.Value,
            recordBookCreation.Value,
            stateCreation.Value,
            this
        );
        _students.Add(student);
        return Result<Student>.Success(student);
    }

    public Result<Student> RemoveStudent(Student student)
    {
        if (_students.Any(s => s.Id == student.Id) == false)
            return Result<Student>.Failure(StudentErrors.StudentDoesntBelongGroup(this));

        _students.Remove(student);
        return Result<Student>.Success(student);
    }

    public Result<Student> GetStudent(
        string name,
        string surname,
        string? patronymic,
        string state,
        ulong recordbook
    )
    {
        Student? student = _students.FirstOrDefault(s =>
            s.Name.Name == name
            && s.Name.Surname == surname
            && s.Name.Patronymic == patronymic
            && s.Recordbook.Recordbook == recordbook
            && s.State.State == state
        );

        return student == null
            ? Result<Student>.Failure(StudentErrors.NotFound())
            : Result<Student>.Success(student);
    }

    public Result<StudentGroup> MergeWithGroup(StudentGroup group)
    {
        if (Id == group.Id)
            return Result<StudentGroup>.Failure(StudentGroupErrors.CantMergeWithSame());

        for (int index = 0; index < group._students.Count; index++)
        {
            group._students[index].ChangeGroup(this);
            _students.Add(group._students[index]);
        }

        return Result<StudentGroup>.Success(this);
    }
}
