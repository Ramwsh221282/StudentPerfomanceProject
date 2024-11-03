using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments.ValueObjects;

public class AssignmentValue : DomainValueObject
{
	public static AssignmentValue VeryGood = new AssignmentValue(5);

	public static AssignmentValue Good = new AssignmentValue(4);

	public static AssignmentValue Satysfine = new AssignmentValue(3);

	public static AssignmentValue Bad = new AssignmentValue(2);

	public static AssignmentValue NotAttestated = new AssignmentValue(0);

	internal static AssignmentValue Empty => new AssignmentValue(1);

	private static byte[] _values = [0, 2, 3, 4, 5];

	public byte Value { get; init; }

	private AssignmentValue() { }

	private AssignmentValue(byte value)
	{
		Value = value;
	}

	public static Result<AssignmentValue> Create(byte value)
	{
		if (_values.Any(v => v == value) == false)
			return AssignmentErrors.InvalidAssignmentValue(value);

		return new AssignmentValue(value);
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		throw new NotImplementedException();
	}
}
