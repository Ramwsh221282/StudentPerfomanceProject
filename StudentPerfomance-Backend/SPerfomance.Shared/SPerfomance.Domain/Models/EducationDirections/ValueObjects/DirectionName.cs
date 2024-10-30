using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.ValueObjects;

public class DirectionName : DomainValueObject
{
	private const int nameMaxLength = 100;

	private const int nameMinLength = 10;

	public string Name { get; private set; }

	private DirectionName(string name) => Name = name;

	private DirectionName() => Name = string.Empty;

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Name;
	}

	public static DirectionName Empty => new DirectionName();

	public static Result<DirectionName> Create(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
			return Result<DirectionName>.Failure(EducationDirectionErrors.NameEmptyError());

		if (name.Length > nameMaxLength)
			return Result<DirectionName>.Failure(EducationDirectionErrors.NameExceessLengthError(nameMaxLength));

		if (name.Length < nameMinLength)
			return Result<DirectionName>.Failure(EducationDirectionErrors.NameIsNotSatisfineError(nameMinLength));

		return Result<DirectionName>.Success(new DirectionName(name));
	}
}
