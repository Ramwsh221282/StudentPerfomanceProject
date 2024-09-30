using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;

public sealed class EducationPlanWithoutDirectionError : Error
{
	public EducationPlanWithoutDirectionError() => error = "Не указано направление подготовки";
	public override string ToString() => error;
}
