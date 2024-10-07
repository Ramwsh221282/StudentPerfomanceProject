using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;

public sealed class SemesterPlanDisciplineNameExceessLengthError : Error
{
	public SemesterPlanDisciplineNameExceessLengthError(int maxLength) => error = $"Название дисциплины превышает длину ${maxLength}";
	public override string ToString() => error.ToString();
}
