using CSharpFunctionalExtensions;
using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Semesters;

namespace StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

public class SemesterNumber : ValueObject
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
	public SemesterNumber() { }
	private SemesterNumber(byte value) => Value = value;
	public byte Value { get; }
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}

	public static Result<SemesterNumber> Create(byte value)
	{
		SemesterNumber number = new SemesterNumber(value);
		Validator<SemesterNumber> validator = new SemesterNumberValidator(number);
		return validator.Validate() switch
		{
			true => number,
			false => Result.Failure<SemesterNumber>(validator.GetErrorText()),
		};
	}
}
