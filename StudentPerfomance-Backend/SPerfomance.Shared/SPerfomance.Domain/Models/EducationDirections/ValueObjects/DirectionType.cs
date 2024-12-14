using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationDirections.ValueObjects;

public class DirectionType : DomainValueObject
{
    public static readonly DirectionType Magister = new("Магистратура");

    public static readonly DirectionType Bachelor = new("Бакалавриат");

    public string Type { get; }

    private DirectionType() => Type = string.Empty;

    private DirectionType(string type) => Type = type.Trim();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
    }

    public static DirectionType Empty => new DirectionType();

    public static Result<DirectionType> Create(string? type)
    {
        if (string.IsNullOrWhiteSpace(type))
            return Result<DirectionType>.Failure(EducationDirectionErrors.TypeEmptyError());

        DirectionType[] types = [Magister, Bachelor];

        return types.Any(t => t.Type == type) == false
            ? Result<DirectionType>.Failure(EducationDirectionErrors.TypeInvalidError(type))
            : Result<DirectionType>.Success(new DirectionType(type));
    }
}
