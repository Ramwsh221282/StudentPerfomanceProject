using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class PasswordNotValidError : Error
{
	public PasswordNotValidError() => error = "Введеный пароль некорректнен";

	public override string ToString() => error;
}
