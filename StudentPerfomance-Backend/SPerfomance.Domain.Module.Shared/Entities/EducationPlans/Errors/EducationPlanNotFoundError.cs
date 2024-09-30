using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

public sealed class EducationPlanNotFoundError : Error
{
	public EducationPlanNotFoundError() => error = "Учебный план не найден";
	public override string ToString() => error;
}
