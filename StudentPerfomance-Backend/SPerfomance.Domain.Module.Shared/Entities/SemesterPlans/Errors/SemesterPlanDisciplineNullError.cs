using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class SemesterPlanDisciplineNullError : Error
{
	public SemesterPlanDisciplineNullError() => error = "Дисциплина не была указана";
	public override string ToString() => error;
}
