using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class SemesterPlanNotFoundError : Error
{
	public SemesterPlanNotFoundError() => error = "Не найдена дисциплина семестра";
	public override string ToString() => error.ToString();
}
