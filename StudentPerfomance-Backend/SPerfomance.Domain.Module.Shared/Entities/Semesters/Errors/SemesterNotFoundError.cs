using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

public sealed class SemesterNotFoundError : Error
{
	public SemesterNotFoundError() => error = "Не найден семестр";
	public override string ToString() => error;
}
