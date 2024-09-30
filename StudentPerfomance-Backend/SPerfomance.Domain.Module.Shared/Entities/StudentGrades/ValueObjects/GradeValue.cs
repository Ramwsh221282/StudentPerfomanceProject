using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.StudentGrades.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.StudentGrades.ValueObjects;

public sealed class GradeValue : ValueObject
{
	private GradeValue()
	{
		Value = string.Empty;
	}

	public static GradeValue NotAttestated = new GradeValue("н/а");
	public static GradeValue Bad = new GradeValue("2");
	public static GradeValue Satisfine = new GradeValue("3");
	public static GradeValue Good = new GradeValue("4");
	public static GradeValue VeryGood = new GradeValue("5");

	private GradeValue(string value) => Value = value;

	public string Value { get; }
	public static GradeValue CreateDefault() => new GradeValue();
	public static CSharpFunctionalExtensions.Result<GradeValue> Create(string value)
	{
		GradeValue result = new GradeValue(value);
		Validator<GradeValue> validator = new GradeValueValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<GradeValue>(validator.GetErrorText());
	}

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}
	public override string ToString() => Value;
}
