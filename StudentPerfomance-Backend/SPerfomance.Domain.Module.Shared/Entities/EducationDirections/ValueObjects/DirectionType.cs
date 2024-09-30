using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;

public class DirectionType : ValueObject
{
	protected DirectionType() => Type = string.Empty;
	protected DirectionType(string type) => Type = type;
	public string Type { get; protected set; } = null!;
	public static DirectionType CreateDefault() => new DirectionType();
	public static CSharpFunctionalExtensions.Result<DirectionType> Create(string type)
	{
		DirectionType result = new DirectionType(type);
		Validator<DirectionType> validator = new DirectionTypeValidator(result);
		return validator.Validate() ? result : CSharpFunctionalExtensions.Result.Failure<DirectionType>(validator.GetErrorText());
	}
	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Type;
	}

	public override string ToString() => Type;
}
