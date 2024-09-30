using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

public sealed class SemesterNumberError : Error
{
	public SemesterNumberError() => error = "Некорректный номер семестра";
	public override string ToString() => error;
}
