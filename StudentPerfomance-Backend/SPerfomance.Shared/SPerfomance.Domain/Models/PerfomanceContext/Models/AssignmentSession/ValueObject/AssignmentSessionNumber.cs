using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;

public sealed class AssignmentSessionNumber : DomainValueObject
{
    private static readonly byte[] AllowedNumbers = [1, 2];

    public byte Number { get; init; }

    private AssignmentSessionNumber(byte number) => Number = number;

    public static Result<AssignmentSessionNumber> Create(byte number)
    {
        if (AllowedNumbers.Any(n => n == number) == false)
            return AssignmentSessionErrors.AssignmentSessionNumberNotAllowed(number);

        return new AssignmentSessionNumber(number);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}
