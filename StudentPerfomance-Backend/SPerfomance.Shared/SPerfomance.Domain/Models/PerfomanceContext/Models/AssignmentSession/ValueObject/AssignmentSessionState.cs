using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;

public class AssignmentSessionState : DomainValueObject
{
    public bool State { get; init; }

    public static AssignmentSessionState Opened = new() { State = true };

    public static AssignmentSessionState Closed = new() { State = false };

    private AssignmentSessionState() { }

    private AssignmentSessionState(AssignmentSessionState state) => State = state.State;

    public static AssignmentSessionState Create(bool state) => new(state);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }

    public static implicit operator AssignmentSessionState(bool state) => new() { State = state };
}
