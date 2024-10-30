using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.ValueObjects;

public class DirectionType : DomainValueObject
{
	internal static DirectionType Magister = new DirectionType("Магистратура");

	internal static DirectionType Bachelor = new DirectionType("Бакалавриат");

	public string Type { get; private set; }

	private DirectionType() => Type = string.Empty;

	private DirectionType(string type) => Type = type;

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Type;
	}

	public static DirectionType Empty => new DirectionType();

	public static Result<DirectionType> Create(string type)
	{
		if (string.IsNullOrWhiteSpace(type))
			return Result<DirectionType>.Failure(EducationDirectionErrors.TypeEmptyError());

		DirectionType[] types = [Magister, Bachelor];
		if (types.Any(t => t.Type == type) == false)
			return Result<DirectionType>.Failure(EducationDirectionErrors.TypeInvalidError(type));

		return Result<DirectionType>.Success(new DirectionType(type));
	}
}
