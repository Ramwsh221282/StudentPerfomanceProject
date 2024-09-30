using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

public sealed class SemesterNumberTypeError : Error
{
	public SemesterNumberTypeError() => error = "Недопустимое значение номера семестра";
	public override string ToString() => error;
}
