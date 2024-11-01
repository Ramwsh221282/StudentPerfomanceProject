using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.ValueObjects;

public class AssignmentWeekState : DomainValueObject
{
	public static AssignmentWeekState Closed = new AssignmentWeekState() { State = false };

	public static AssignmentWeekState Opened = new AssignmentWeekState() { State = true };

	public bool State { get; private set; }

	private AssignmentWeekState() { }

	private AssignmentWeekState(AssignmentWeekState state) => State = state.State;

	public static AssignmentWeekState Create(AssignmentWeekState state) => new AssignmentWeekState(state);

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return State;
	}
}
