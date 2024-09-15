using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.ValueObjects;
using StudentPerfomance.Domain.Validators.Student;
using StudentPerfomance.Domain.ValueObjects.Student;

namespace StudentPerfomance.Domain.Entities;

public class Student : Entity
{
    private Student() : base(Guid.Empty) { }
    private Student(Guid id, StudentName name, StudentState state, StudentRecordBook recordbook, StudentGroup group) : base(id)
    {
        Name = name;
        State = state;
        Recordbook = recordbook;
        Group = group;
    }
    public StudentGroup? Group { get; private set; }
    public StudentName Name { get; private set; } = null!;
    public StudentState State { get; private set; } = null!;
    public StudentRecordBook Recordbook { get; private set; } = null!;

    public void ChangeGroup(StudentGroup group) => Group = group;

    public void ChangeName(StudentName name) => Name = name;

    public void ChangeState(StudentState state) => State = state;

    public void ChangeRecordBook(StudentRecordBook recordBook) => Recordbook = recordBook;

    public static Result<Student> Create(Guid id, StudentName name, StudentState state, StudentRecordBook recordBook, StudentGroup group)
    {
        Student student = new Student(id, name, state, recordBook, group);
        Validator<Student> validator = new StudentValidator(student);
        return validator.Validate() switch
        {
            true => student,
            false => Result.Failure<Student>(validator.GetErrorText())
        };
    }            
}
