using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

public sealed class SemesterNumber : ValueObject
{
	public static SemesterNumber OneSemester = new SemesterNumber(1);
	public static SemesterNumber TwoSemester = new SemesterNumber(2);
	public static SemesterNumber ThreeSemester = new SemesterNumber(3);
	public static SemesterNumber FourSemester = new SemesterNumber(4);
	public static SemesterNumber FiveSemester = new SemesterNumber(5);
	public static SemesterNumber SixSemester = new SemesterNumber(6);
	public static SemesterNumber SevenSemenster = new SemesterNumber(7);
	public static SemesterNumber EightSemester = new SemesterNumber(8);
	public static SemesterNumber NineSemester = new SemesterNumber(9);
	public static SemesterNumber TenSemester = new SemesterNumber(10);
	public static SemesterNumber ElevenSemester = new SemesterNumber(11);
	private SemesterNumber() => Value = default;
	private SemesterNumber(byte value) => Value = value;
	public byte Value { get; }
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}

	public static CSharpFunctionalExtensions.Result<SemesterNumber> Create(byte value)
	{
		SemesterNumber number = new SemesterNumber(value);
		Validator<SemesterNumber> validator = new SemesterNumberValidator(number);
		return validator.Validate() ? number : CSharpFunctionalExtensions.Result.Failure<SemesterNumber>(validator.GetErrorText());
	}

	public static SemesterNumber CreateDefault() => new SemesterNumber();
	public override string ToString() => Value.ToString();
}
