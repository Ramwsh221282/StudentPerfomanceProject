using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public class WorkingConditionTypeError : Error
{
	public WorkingConditionTypeError() => error = "Некорректное значение условия работы";
	public override string ToString() => error;
}
