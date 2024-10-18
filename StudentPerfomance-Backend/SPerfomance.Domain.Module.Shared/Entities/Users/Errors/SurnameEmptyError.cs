using SPerfomance.Domain.Module.Shared.Common.Abstractions.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

public class SurnameEmptyError : Error
{
	public SurnameEmptyError() => error = "Фамилия пользователя пустая";

	public override string ToString() => error;
}
