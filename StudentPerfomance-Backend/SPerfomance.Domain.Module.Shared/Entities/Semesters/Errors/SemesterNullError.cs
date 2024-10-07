using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

public sealed class SemesterNullError : Error
{
	public SemesterNullError() => error = "Объект семестра был пустым";
	public override string ToString() => error;
}
