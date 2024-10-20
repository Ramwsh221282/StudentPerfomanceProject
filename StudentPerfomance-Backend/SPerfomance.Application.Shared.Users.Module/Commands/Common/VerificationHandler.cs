using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.SharedServices.Auth;
using SPerfomance.Application.Shared.Module.SharedServices.Auth.Errors;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

namespace SPerfomance.Application.Shared.Users.Module.Commands.Common;

public class VerificationHandler<TCommand, TCommandReuslt> : ICommandHandler<TCommand, TCommandReuslt>
where TCommand : ICommand
{
	private readonly JwtVerificationObject _verification;
	private readonly string _role;
	private readonly UserVerificationService _service;
	protected readonly UsersQueryRepository _repository;

	public VerificationHandler(string token, string role)
	{
		_verification = new JwtVerificationObject(token);
		_role = role;
		_repository = new UsersQueryRepository();
		_service = new UserVerificationService();
	}

	public virtual async Task<OperationResult<TCommandReuslt>> Handle(TCommand command)
	{
		if (_verification.IsExpired)
			return new OperationResult<TCommandReuslt>(new UserSessionExpiredError().ToString());

		User? user = await _repository.GetById(_verification.UserId);
		if (user == null)
			return new OperationResult<TCommandReuslt>(new UserNotFoundError().ToString());

		Result roleVerification = _service.VerifyRole(user, _role);
		if (roleVerification.IsFailure)
			return new OperationResult<TCommandReuslt>(roleVerification.Error);

		user.UpdateLoginDate();
		await _repository.Commit();

		return new OperationResult<TCommandReuslt>().SetNotFailedFlag();
	}
}
