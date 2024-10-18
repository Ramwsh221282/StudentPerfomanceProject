using Microsoft.AspNetCore.Mvc;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Users;

public record class UserSchema : EntitySchema
{
	public string Email { get; init; } = string.Empty;
	public string Name { get; init; } = string.Empty;
	public string Surname { get; init; } = string.Empty;
	public string Thirdname { get; init; } = string.Empty;

	public string HashedPassword { get; set; } = string.Empty;
	public string Role { get; set; } = string.Empty;
	public DateTime RegisteredDate { get; set; }
	public DateTime LastLoginDate { get; set; }

	public UserSchema() { }

	public UserSchema(
		string email,
		string name,
		string surname,
		string thirdname
	)
	{
		if (!string.IsNullOrWhiteSpace(email)) Email = email;
		if (!string.IsNullOrWhiteSpace(name)) Name = name;
		if (!string.IsNullOrWhiteSpace(surname)) Surname = surname;
		if (!string.IsNullOrWhiteSpace(thirdname)) Thirdname = thirdname;
	}

	public Result<EmailValueObject> CreateEmail() => EmailValueObject.Create(Email);

	public Result<Username> CreateUsername() => Username.Create(Name, Surname, Thirdname);

	public Result<UserRole> CreateUserRole() => UserRole.Create(Role);

	public Result<User> CreateDomainObject() => User.Create(CreateEmail().Value, CreateUsername().Value, CreateUserRole().Value, HashedPassword);
}

public static class UserSchemaExtensions
{
	public static UserSchema ToSchema(this User user)
	{
		UserSchema schema = new UserSchema(
			user.Email.Email,
			user.Name.Name,
			user.Name.Surname,
			string.IsNullOrWhiteSpace(user.Name.Thirdname) ? string.Empty : user.Name.Thirdname
		);

		schema.HashedPassword = user.HashedPassword;
		schema.Role = user.Role.Value;
		schema.RegisteredDate = user.RegisteredDate;
		schema.LastLoginDate = user.LastLoginDate;

		return schema;
	}

	public static ActionResult<UserSchema> ToActionResult(this OperationResult<User> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<UserSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<User>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
