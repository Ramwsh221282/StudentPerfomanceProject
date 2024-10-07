using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class SemesterPlanDisciplineNameEmptyError : Error
{
	public SemesterPlanDisciplineNameEmptyError() => error = "Имя дисциплины было пустое";
	public override string ToString() => error.ToString();
}
