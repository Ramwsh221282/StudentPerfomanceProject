using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationPlans.ValueObjects;

public class PlanYear : DomainValueObject
{
    private const int MaxLength = 3000;
    private const int MinLength = 2000;

    public int Year { get; private set; }

    private PlanYear() { }

    private PlanYear(int year) => Year = year;

    public static PlanYear Default => new PlanYear();

    public static Result<PlanYear> Create(int year)
    {
        return year switch
        {
            > MaxLength => Result<PlanYear>.Failure(
                EducationPlanErrors.YearExceessError(MaxLength)
            ),
            < MinLength => Result<PlanYear>.Failure(EducationPlanErrors.YearLessError(MinLength)),
            _ => Result<PlanYear>.Success(new PlanYear(year)),
        };
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
    }
}
