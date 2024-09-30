using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

public sealed class JobTitleTypeError : Error
{
	public JobTitleTypeError() => error = "Такой должности быть не может";
	public override string ToString() => error;
}
