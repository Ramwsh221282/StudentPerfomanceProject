using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class RoleNotSupportedError : Error
{
	public RoleNotSupportedError() => error = "Такая роль не поддерживается";
}
