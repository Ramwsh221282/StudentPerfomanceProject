using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Validators;

// Валидация года набора.
internal sealed class YearOfCreationValidator(YearOfCreation year) : Validator<YearOfCreation>
{
	private const uint MAX_VALUE_SIZE = 3000;
	private const uint MIN_VALUE_SIZE = 2000;
	private readonly YearOfCreation _year = year;
	public override bool Validate()
	{
		if (_year.Year < MIN_VALUE_SIZE) error.AppendError(new EducationPlanIncorrectYearError(MIN_VALUE_SIZE, MAX_VALUE_SIZE));
		if (_year.Year > MAX_VALUE_SIZE) error.AppendError(new EducationPlanIncorrectYearError(MIN_VALUE_SIZE, MAX_VALUE_SIZE));
		return HasError;
	}
}
