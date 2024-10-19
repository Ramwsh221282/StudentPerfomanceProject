using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class PermissionDeniedError : Error
{
	public PermissionDeniedError() => error = "Отказано в доступе";

	public override string ToString() => error;
}
