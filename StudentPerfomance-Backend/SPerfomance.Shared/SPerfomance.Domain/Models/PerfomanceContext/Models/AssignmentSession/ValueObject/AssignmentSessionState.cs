using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.ValueObject;

public class AssignmentSessionState : DomainValueObject
{
    public bool State { get; init; }

    public static AssignmentSessionState Opened = new AssignmentSessionState() { State = true };

    public static AssignmentSessionState Closed = new AssignmentSessionState() { State = false };

    private AssignmentSessionState() { }

    private AssignmentSessionState(AssignmentSessionState state) => State = state.State;

    public static AssignmentSessionState Create(bool state) => new AssignmentSessionState(state);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }

    public static implicit operator AssignmentSessionState(bool state) =>
        new AssignmentSessionState() { State = state };
}
