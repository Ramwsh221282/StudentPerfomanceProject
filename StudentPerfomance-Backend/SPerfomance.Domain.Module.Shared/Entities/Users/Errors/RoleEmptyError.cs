using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class RoleEmptyError : Error
{
	public RoleEmptyError() => error = "Роль не была задана";
}
