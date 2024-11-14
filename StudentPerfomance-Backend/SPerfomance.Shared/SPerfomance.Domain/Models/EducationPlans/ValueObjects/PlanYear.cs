using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.EducationPlans.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.EducationPlans.ValueObjects;

public class PlanYear : DomainValueObject
{
    private const int MAX_LENGTH = 3000;
    private const int MIN_LENGTH = 2000;

    public int Year { get; private set; }

    private PlanYear() { }

    private PlanYear(int year) => Year = year;

    public static PlanYear Default => new PlanYear();

    public static Result<PlanYear> Create(int year)
    {
        if (year > MAX_LENGTH)
            return Result<PlanYear>.Failure(EducationPlanErrors.YearExceessError(MAX_LENGTH));

        if (year < MIN_LENGTH)
            return Result<PlanYear>.Failure(EducationPlanErrors.YearLessError(MIN_LENGTH));

        return Result<PlanYear>.Success(new PlanYear(year));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
    }
}
