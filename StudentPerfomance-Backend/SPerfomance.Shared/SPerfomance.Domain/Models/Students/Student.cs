using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Students;

public class Student : DomainEntity
{
    public StudentName Name { get; private set; }

    public StudentState State { get; private set; }

    public StudentRecordbook Recordbook { get; private set; }

    public StudentGroup AttachedGroup { get; private set; }

    private Student()
        : base(Guid.Empty)
    {
        Name = StudentName.Empty;
        State = StudentState.Empty;
        Recordbook = StudentRecordbook.Empty;
        AttachedGroup = StudentGroup.Empty;
    }

    internal Student(
        StudentName name,
        StudentRecordbook recordbook,
        StudentState state,
        StudentGroup group
    )
        : base(Guid.NewGuid())
    {
        Name = name;
        Recordbook = recordbook;
        State = state;
        AttachedGroup = group;
    }

    internal static Student Empty => new Student();

    public Result<Student> ChangeName(string name, string surname, string? patronymic)
    {
        Result<StudentName> newName = StudentName.Create(name, surname, patronymic);
        if (newName.IsFailure)
            return Result<Student>.Failure(newName.Error);

        if (Name == newName.Value)
            return Result<Student>.Success(this);

        Name = newName.Value;
        return Result<Student>.Success(this);
    }

    public Result<Student> ChangeRecordBook(ulong recordBook)
    {
        Result<StudentRecordbook> newRecordbook = StudentRecordbook.Create(recordBook);
        if (newRecordbook.IsFailure)
            return Result<Student>.Failure(newRecordbook.Error);

        if (Recordbook == newRecordbook.Value)
            return Result<Student>.Success(this);

        Recordbook = newRecordbook.Value;
        return Result<Student>.Success(this);
    }

    public Result<Student> ChangeState(string state)
    {
        Result<StudentState> newState = StudentState.Create(state);
        if (newState.IsFailure)
            return Result<Student>.Failure(newState.Error);

        if (State == newState.Value)
            return Result<Student>.Success(this);

        State = newState.Value;
        return Result<Student>.Success(this);
    }

    public Result<Student> ChangeGroup(StudentGroup group)
    {
        if (AttachedGroup.Id == group.Id)
            return Result<Student>.Success(this);

        AttachedGroup = group;
        return Result<Student>.Success(this);
    }
}
