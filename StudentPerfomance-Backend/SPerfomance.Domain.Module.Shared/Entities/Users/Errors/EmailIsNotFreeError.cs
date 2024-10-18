using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public sealed class EmailIsNotFreeError : Error
{
	public EmailIsNotFreeError(string email) => error = $"Почта {email} уже занята";

	public override string ToString() => error;
}
