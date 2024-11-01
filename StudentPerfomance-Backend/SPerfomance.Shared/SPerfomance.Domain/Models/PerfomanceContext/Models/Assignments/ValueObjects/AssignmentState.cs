using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;

public class AssignmentState : DomainValueObject
{
	public static AssignmentState Opened = new AssignmentState() { State = true };

	public static AssignmentState Locked = new AssignmentState() { State = false };

	private AssignmentState() { }

	public bool State { get; init; } = false;

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return State;
	}
}
