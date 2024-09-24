using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.EducationDIrections;

namespace StudentPerfomance.Domain.ValueObjects.EducationDirections;

public class DirectionType : ValueObject
{
	protected DirectionType() => Type = string.Empty;
	protected DirectionType(string type) => Type = type;
	public string Type { get; protected set; } = null!;
	public static DirectionType CreateDefault() => new DirectionType();
	public static Result<DirectionType> Create(string type)
	{
		DirectionType result = new DirectionType(type);
		Validator<DirectionType> validator = new DirectionTypeValidator(result);
		return validator.Validate() ? result : Result.Failure<DirectionType>(validator.GetErrorText());
	}
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Type;
	}
}
