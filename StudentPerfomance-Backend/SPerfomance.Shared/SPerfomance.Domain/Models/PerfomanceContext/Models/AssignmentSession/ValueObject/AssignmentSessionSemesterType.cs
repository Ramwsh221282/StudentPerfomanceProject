using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;

public sealed class AssignmentSessionSemesterType : DomainValueObject
{
    private static readonly string[] AllowedTypes = ["Весна", "Осень"];

    public string Type { get; init; }

    private AssignmentSessionSemesterType(string type) => Type = type.Trim();

    public static Result<AssignmentSessionSemesterType> Create(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            return AssignmentSessionErrors.AssignmentSessionSemesterTypeEmpty();

        if (AllowedTypes.Any(t => t == type) == false)
            return AssignmentSessionErrors.AssignmentSessionSemesterTypeInvalid(type);

        return new AssignmentSessionSemesterType(type);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
    }
}
