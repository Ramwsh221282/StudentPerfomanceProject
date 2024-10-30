using SPerfomance.Domain.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Models.Users.ValueObjects;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Domain.Models.Users;

public class User : DomainEntity
{
	public Username Name { get; private set; }

	public UserEmail Email { get; private set; }

	public UserRole Role { get; private set; }

	public string HashedPassword { get; private set; }

	public DateTime RegisteredDate { get; init; }

	public DateTime LastLoginDate { get; private set; }

	public string AttachedRoleId { get; private set; }

	private User() : base(Guid.Empty)
	{
		Name = Username.Empty;
		Email = UserEmail.Empty;
		Role = UserRole.Empty;
		HashedPassword = string.Empty;
		RegisteredDate = DateTime.MinValue;
		LastLoginDate = DateTime.MinValue;
		AttachedRoleId = string.Empty;
	}

	private User(
		Username username,
		UserEmail email,
		UserRole role,
		string hashedPassword
		) : base(Guid.NewGuid())
	{
		Name = username;
		Email = email;
		Role = role;
		HashedPassword = hashedPassword;
		RegisteredDate = DateTime.Now;
		LastLoginDate = DateTime.Now;
		AttachedRoleId = string.Empty;
	}

	internal static User Empty => new User();

	public void UpdateLoginDate() => LastLoginDate = DateTime.Now;

	public void UpdatePassword(string hashedPassword) => HashedPassword = hashedPassword;

	public void AttachRole(DomainEntity entity) => AttachedRoleId = entity.Id.ToString();

	public static Result<User> Create(
		string name,
		string surname,
		string? patronymic,
		string email,
		string role,
		string hashedPassword)
	{
		Result<Username> nameCreation = Username.Create(name, surname, patronymic);
		if (nameCreation.IsFailure)
			return Result<User>.Failure(nameCreation.Error);

		Result<UserEmail> emailCreation = UserEmail.Create(email);
		if (emailCreation.IsFailure)
			return Result<User>.Failure(emailCreation.Error);

		Result<UserRole> roleCreation = UserRole.Create(role);
		if (roleCreation.IsFailure)
			return Result<User>.Failure(roleCreation.Error);

		if (string.IsNullOrWhiteSpace(hashedPassword))
			return Result<User>.Failure(UserErrors.PasswordEmpty());

		return Result<User>.Success(new(
			nameCreation.Value,
			emailCreation.Value,
			roleCreation.Value,
			hashedPassword
		));
	}

	public Result<User> ChangeEmail(string email)
	{
		Result<UserEmail> newEmail = UserEmail.Create(email);
		if (newEmail.IsFailure)
			return Result<User>.Failure(newEmail.Error);

		if (Email == newEmail.Value)
			return Result<User>.Success(this);

		Email = newEmail.Value;
		return Result<User>.Success(this);
	}

	public Result<User> ChangeName(string name, string surname, string? patronymic)
	{
		Result<Username> newName = Username.Create(name, surname, patronymic);
		if (newName.IsFailure)
			return Result<User>.Failure(newName.Error);

		if (Name == newName.Value)
			return Result<User>.Success(this);

		Name = newName.Value;
		return Result<User>.Success(this);
	}

	public Result<User> ChangeRole(string role)
	{
		Result<UserRole> newRole = UserRole.Create(role);
		if (newRole.IsFailure)
			return Result<User>.Failure(newRole.Error);

		if (Role == newRole.Value)
			return Result<User>.Success(this);

		Role = newRole.Value;
		return Result<User>.Success(this);
	}
}
