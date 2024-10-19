using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Users;

public class User : Entity
{
	public const string Admin = "Администратор";
	public const string Teacher = "Преподаватель";
	public const string AdvancedTeacher = "Преподаватель с правами администратора";

	private User() : base(Guid.Empty)
	{
		Name = Username.CreateDefault();
		Email = EmailValueObject.CreateDefault();
		HashedPassword = string.Empty;
		Role = UserRole.CreateDefault();
		LastLoginDate = DateTime.MinValue;
		RegisteredDate = DateTime.MinValue;
	}

	private User(Guid id, EmailValueObject email, Username name, UserRole role, string hashedPassword) : base(id)
	{
		Name = name;
		Email = email;
		HashedPassword = hashedPassword;
		Role = role;
		LastLoginDate = DateTime.Now;
		RegisteredDate = DateTime.Now;
	}

	public Username Name { get; init; }
	public EmailValueObject Email { get; init; }
	public string HashedPassword { get; private set; }
	public UserRole Role { get; init; }
	public DateTime LastLoginDate { get; private set; }
	public DateTime RegisteredDate { get; init; }

	public User CreateDefault() => new User();

	public static CSharpFunctionalExtensions.Result<User> Create(EmailValueObject email, Username name, UserRole role, string hashedPassword)
	{
		return string.IsNullOrWhiteSpace(hashedPassword) ?
			CSharpFunctionalExtensions.Result.Failure<User>("Пароль не был установлен") :
			new User(Guid.NewGuid(), email, name, role, hashedPassword);
	}

	public void UpdateLoginDate() => LastLoginDate = DateTime.Now;

	public void UpdatePassword(string newHashedPassword) => HashedPassword = newHashedPassword;
}
