using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class JobTitleNullError : Error
{
	public JobTitleNullError() => error = "Должность была пустая";
	public override string ToString() => error;
}
