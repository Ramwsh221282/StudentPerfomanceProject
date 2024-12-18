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

/// <summary>
/// Сущность - студенческая группа.
/// </summary>
public class StudentGroup : DomainEntity
{
    /// <summary>
    /// Список студентов.
    /// </summary>
    private readonly List<Student> _students = [];

    /// <summary>
    /// Статистика группы в контрольной недели.
    /// </summary>
    private List<AssignmentWeek> _weeks = [];

    /// <summary>
    /// Название группы.
    /// </summary>
    public StudentGroupName Name { get; private set; }

    /// <summary>
    /// Учебный план группы.
    /// </summary>
    public EducationPlan? EducationPlan { get; private set; }

    /// <summary>
    /// Активный семестр группы.
    /// </summary>
    public Semester? ActiveGroupSemester { get; private set; }

    /// <summary>
    /// Список студентов группы (только для чтения).
    /// </summary>
    public IReadOnlyCollection<Student> Students => _students;

    /// <summary>
    /// Список статистики группы в контрольных неделях.
    /// </summary>
    public IReadOnlyCollection<AssignmentWeek> Weeks => _weeks;

    /// <summary>
    /// Закрытый конструктор создания объекта. Нужен для ORM.
    /// </summary>
    private StudentGroup()
        : base(Guid.Empty)
    {
        Name = StudentGroupName.Empty;
    }

    /// <summary>
    /// Закрытый конструктор создания объекта.
    /// </summary>
    private StudentGroup(StudentGroupName name)
        : base(Guid.NewGuid())
    {
        Name = name;
    }

    /// <summary>
    /// Создание дефолтного объекта (без проинициализированных значений).
    /// </summary>
    internal static StudentGroup Empty => new StudentGroup();

    /// <summary>
    /// Статичный фабричный метод создания экземпляра класса StudentGroup
    /// </summary>
    /// <param name="name">Название группы</param>
    /// <returns>Результат создания экземпляра класса студенческой группы. Возаращет Result.Error или Result.Success(Value)</returns>
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

    public Result<Student> AddStudent(Student student)
    {
        if (
            _students.Any(s =>
                s.Name.Name == student.Name.Name
                && s.Name.Surname == student.Name.Surname
                && s.Name.Patronymic == student.Name.Patronymic
                && s.Recordbook.Recordbook == student.Recordbook.Recordbook
                && s.State.State == student.State.State
            )
        )
            return Result<Student>.Failure(StudentErrors.StudentDublicateError(this));
        _students.Add(student);
        return student;
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

    public Result<Student> GetStudent(Func<Student, bool> predicate)
    {
        Student? student = _students.FirstOrDefault(predicate);
        return student == null ? StudentErrors.NotFound() : student;
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
