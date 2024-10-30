using SPerfomance.Domain.Abstractions;

namespace SPerfomance.Domain.Models.Semesters.ValueObjects;

public class SemesterNumber : DomainValueObject
{
	public byte Number { get; init; }

	internal SemesterNumber() { }

	internal SemesterNumber(byte number) => Number = number;

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Number;
	}
}
