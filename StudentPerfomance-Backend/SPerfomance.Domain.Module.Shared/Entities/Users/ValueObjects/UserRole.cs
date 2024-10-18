using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Users.Validators;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

public class UserRole : ValueObject
{
	public string Value { get; private set; }

	private UserRole() => Value = string.Empty;

	private UserRole(string value) => Value = value;

	public override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
	}

	public CSharpFunctionalExtensions.Result ChangeRole(UserRole role)
	{
		if (this == role)
			return CSharpFunctionalExtensions.Result.Failure("Роль идентична");

		Value = role.Value;
		return CSharpFunctionalExtensions.Result.Success();
	}

	public static UserRole CreateDefault() => new UserRole();

	public static CSharpFunctionalExtensions.Result<UserRole> Create(string value)
	{
		UserRole result = new UserRole(value);
		Validator<UserRole> validator = new UserRoleValidator(result);
		return validator.Validate() ?
			result :
			CSharpFunctionalExtensions.Result.Failure<UserRole>(validator.GetErrorText());
	}
}
