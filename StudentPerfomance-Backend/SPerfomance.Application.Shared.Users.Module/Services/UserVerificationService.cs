using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Users.Module.Services;

public sealed class UserVerificationService
{
	public Result VerifyRole(User user, string role)
	{
		Result<UserRole> roleRequest = UserRole.Create(role);
		if (roleRequest.IsFailure)
			return Result.Failure(new RoleNotSupportedError().ToString());

		return user.Role == roleRequest.Value ?
			Result.Success() :
			Result.Failure(new PermissionDeniedError().ToString());
	}
}
