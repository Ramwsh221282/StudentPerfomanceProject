using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.ValueObjects;

public class DirectionName : DomainValueObject
{
    private const int NameMaxLength = 100;

    private const int NameMinLength = 5;

    public string Name { get; private set; }

    private DirectionName(string name) => Name = name;

    private DirectionName() => Name = string.Empty;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public static DirectionName Empty => new DirectionName();

    public static Result<DirectionName> Create(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<DirectionName>.Failure(EducationDirectionErrors.NameEmptyError());

        return name.Length switch
        {
            > NameMaxLength => Result<DirectionName>.Failure(
                EducationDirectionErrors.NameExceessLengthError(NameMaxLength)
            ),
            < NameMinLength => Result<DirectionName>.Failure(
                EducationDirectionErrors.NameIsNotSatisfineError(NameMinLength)
            ),
            _ => Result<DirectionName>.Success(new DirectionName(name)),
        };
    }
}
