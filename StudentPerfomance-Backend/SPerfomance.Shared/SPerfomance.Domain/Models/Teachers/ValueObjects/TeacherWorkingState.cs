using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Teachers.ValueObjects;

public class TeacherWorkingState : DomainValueObject
{
    private static TeacherWorkingState[] _states =
    [
        new("Штатный"),
        new("Удаленный совместитель"),
        new("Совместиитель"),
    ];

    public string State { get; private set; }

    private TeacherWorkingState() => State = string.Empty;

    private TeacherWorkingState(string state) => State = state;

    internal static TeacherWorkingState Empty => new TeacherWorkingState();

    internal static Result<TeacherWorkingState> Create(string state)
    {
        if (string.IsNullOrWhiteSpace(state))
            return Result<TeacherWorkingState>.Failure(TeacherErrors.WorkingStateEmpty());

        if (_states.Any(s => s.State == state) == false)
            return Result<TeacherWorkingState>.Failure(TeacherErrors.WorkingStateInvalid(state));

        return Result<TeacherWorkingState>.Success(new TeacherWorkingState(state));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }
}
