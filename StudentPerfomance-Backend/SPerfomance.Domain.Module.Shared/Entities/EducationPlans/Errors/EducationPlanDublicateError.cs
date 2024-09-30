using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

public sealed class EducationPlanDublicateError : Error
{
	public EducationPlanDublicateError() => error = "Учебный план уже существует";
	public override string ToString() => error;
}
