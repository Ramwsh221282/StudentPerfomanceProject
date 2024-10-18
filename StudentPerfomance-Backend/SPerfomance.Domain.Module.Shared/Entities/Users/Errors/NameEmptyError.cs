using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public sealed class NameEmptyError : Error
{
	public NameEmptyError() => error = "Имя пользователя было пустое";

	public override string ToString() => error;
}
