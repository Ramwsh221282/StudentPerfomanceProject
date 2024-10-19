using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Application.Shared.Module.SharedServices.Auth.Errors;

public class UserSessionExpiredError : Error
{
	public UserSessionExpiredError() => error = "Сессия текущего пользователя не существует/закончена.\nНеобходимо пройти процедуру авторизации";

	public override string ToString() => error;
}
