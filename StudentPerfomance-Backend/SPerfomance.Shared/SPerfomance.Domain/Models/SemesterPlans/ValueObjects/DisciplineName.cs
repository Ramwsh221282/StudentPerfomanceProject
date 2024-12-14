using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.SemesterPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.SemesterPlans.ValueObjects;

public class DisciplineName : DomainValueObject
{
    private const int MaxNameLength = 150;

    public string Name { get; }

    internal DisciplineName() => Name = string.Empty;

    internal DisciplineName(string name) => Name = name.Trim();

    internal static DisciplineName Empty => new DisciplineName();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    internal static Result<DisciplineName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<DisciplineName>.Failure(SemesterPlanErrors.DisciplineNameEmpty());

        if (name.Length > MaxNameLength)
            return Result<DisciplineName>.Failure(
                SemesterPlanErrors.DisciplineLengthExceess(MaxNameLength)
            );

        return Result<DisciplineName>.Success(new DisciplineName(name));
    }

    public override string ToString() => $"{Name}";
}
