using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class SemesterPlanError : Error
{
	public SemesterPlanError() => error = "Невозможно создать связку семестр + дисциплина";
	public override string ToString() => error;
}
