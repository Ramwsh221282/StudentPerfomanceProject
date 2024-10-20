using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.SharedServices.Auth;
using SPerfomance.Application.Shared.Module.SharedServices.Auth.Errors;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

namespace SPerfomance.Application.Shared.Users.Module.Queries.Common;

public class QueryVerificaitonHandler<TQuery, TQueryResult> : IQueryHandler<TQuery, TQueryResult>
where TQuery : IQuery
{
	private readonly JwtVerificationObject _verification;
	private readonly string _role;
	private readonly UserVerificationService _service;
	protected readonly UsersQueryRepository _repository;

	public QueryVerificaitonHandler(string token, string role)
	{
		_verification = new JwtVerificationObject(token);
		_role = role;
		_repository = new UsersQueryRepository();
		_service = new UserVerificationService();
	}

	public async Task<OperationResult<TQueryResult>> Handle(TQuery query)
	{
		if (_verification.IsExpired)
			return new OperationResult<TQueryResult>(new UserSessionExpiredError().ToString());

		User? user = await _repository.GetById(_verification.UserId);
		if (user == null)
			return new OperationResult<TQueryResult>(new UserNotFoundError().ToString());

		Result roleVerification = _service.VerifyRole(user, _role);
		if (roleVerification.IsFailure)
			return new OperationResult<TQueryResult>(roleVerification.Error);

		user.UpdateLoginDate();
		await _repository.Commit();

		return new OperationResult<TQueryResult>().SetNotFailedFlag();
	}
}
