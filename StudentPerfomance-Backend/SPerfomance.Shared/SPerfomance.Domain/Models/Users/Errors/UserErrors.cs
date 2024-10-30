using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Users.Errors;

public static class UserErrors
{
	public static Error EmailEmpty() => new Error("Почта не была указана");

	public static Error EmailInvalid(string email) => new Error($"Почта {email} некорректна");

	public static Error NameEmpty() => new Error("Имя не указано");

	public static Error SurnameEmpty() => new Error("Фамилия не указана");

	public static Error NameExceess(int length) => new Error($"Имя превышает {length} символов");

	public static Error SurnameExcees(int length) => new Error($"Фамилия превышает {length} символов");

	public static Error ThirdnameExcess(int length) => new Error($"Отчество превышает {length} символов");

	public static Error RoleEmpty() => new Error("Права не указаны");

	public static Error RoleInvalid(string role) => new Error($"Права {role} недопустимы");

	public static Error NotFound() => new Error("Пользователь не найден");

	public static Error PasswordInvalid() => new Error("Неверный пароль");

	public static Error PasswordEmpty() => new Error("Пароль не был указан");

	public static Error EmailDublicate(string email) => new Error($"{email} занята");
}
