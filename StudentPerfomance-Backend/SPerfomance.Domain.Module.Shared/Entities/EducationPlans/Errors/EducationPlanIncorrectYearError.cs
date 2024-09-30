using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

public sealed class EducationPlanIncorrectYearError : Error
{
	public EducationPlanIncorrectYearError(uint minValue, uint maxValue) => error = $"Некорректный год семестра ({minValue}-{maxValue})";
	public override string ToString() => error;
}
