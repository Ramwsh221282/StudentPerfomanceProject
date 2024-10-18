using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class UserNotFoundError : Error
{
	public UserNotFoundError() => error = "Пользователь не найден";

	public override string ToString() => error.ToString();
}
