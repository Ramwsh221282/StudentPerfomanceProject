using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Users.Validators;

internal sealed class UserRoleValidator(UserRole role) : Validator<UserRole>
{
	private readonly UserRole _role = role;

	private readonly string[] _roles = [
		"Администратор",
		"Преподаватель",
		"Преподаватель с правами администратора",
	];

	public override bool Validate()
	{
		if (_role == null)
			error.AppendError(new RoleEmptyError());
		if (_role != null && string.IsNullOrWhiteSpace(_role.Value))
			error.AppendError(new RoleEmptyError());
		if (_role != null && !string.IsNullOrWhiteSpace(_role.Value) &&
		_roles.Any(r => r == _role.Value) == false)
			error.AppendError(new RoleNotSupportedError());
		return HasError;
	}
}
