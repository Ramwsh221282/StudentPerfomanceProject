using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Student;

namespace StudentPerfomance.Domain.ValueObjects;

public class StudentState : ValueObject
{    
    public static readonly StudentState Active = new StudentState("Активен");

    public static readonly StudentState NotActive = new StudentState("Неактивен");

    private StudentState() { }

    private StudentState(string state) => State = state;

    public string State { get; }

    public static Result<StudentState> Create(string state)
    {
        Validator<StudentState> validator = new StudentStateValidator(state);
        if (!validator.Validate())
            return Result.Failure<StudentState>(validator.GetErrorText());
        return new StudentState(state);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }
}
