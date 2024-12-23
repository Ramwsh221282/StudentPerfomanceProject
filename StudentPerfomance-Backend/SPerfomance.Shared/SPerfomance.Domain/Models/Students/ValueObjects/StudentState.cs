using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Students.ValueObjects;

public class StudentState : DomainValueObject
{
    private static StudentState[] _states = [new("Активен"), new("Неактивен")];

    public static readonly StudentState Active = new StudentState("Активен");

    public static readonly StudentState NotActive = new StudentState("Неактивен");

    public string State { get; private set; }

    private StudentState() => State = string.Empty;

    private StudentState(string state) => State = state.Trim();

    internal static StudentState Empty => new StudentState();

    internal static Result<StudentState> Create(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            return Result<StudentState>.Failure(StudentErrors.StateEmpty());

        return _states.Any(s => s.State == state) == false
            ? Result<StudentState>.Failure(StudentErrors.InvalidState(state))
            : Result<StudentState>.Success(new StudentState(state));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }

    public void Change(StudentState state)
    {
        if (this != state)
            State = state.State;
    }
}
