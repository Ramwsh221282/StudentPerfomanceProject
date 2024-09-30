using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public class WorkingConditionNullError : Error
{
	public WorkingConditionNullError() => error = "Условие работы преподавателя было пустым";
	public override string ToString() => error;
}
