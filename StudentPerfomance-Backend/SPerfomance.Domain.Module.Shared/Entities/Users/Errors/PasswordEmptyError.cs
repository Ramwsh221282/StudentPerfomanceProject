using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public sealed class PasswordEmptyError : Error
{
	public PasswordEmptyError() => error = "Пароль не был задан";

	public override string ToString() => error;
}
