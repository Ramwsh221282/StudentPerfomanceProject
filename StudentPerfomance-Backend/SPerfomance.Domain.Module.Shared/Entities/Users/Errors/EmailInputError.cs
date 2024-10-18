using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public sealed class EmailInputError : Error
{
	public EmailInputError() => error = "Некорретный email пользователя";

	public override string ToString() => error;
}
