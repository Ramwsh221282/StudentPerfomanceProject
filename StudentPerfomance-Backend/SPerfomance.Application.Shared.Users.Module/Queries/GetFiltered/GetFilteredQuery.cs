using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.SharedServices.Auth;
using SPerfomance.Application.Shared.Module.SharedServices.Auth.Errors;
using SPerfomance.Application.Shared.Users.Module.API.Responses;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

namespace SPerfomance.Application.Shared.Users.Module.Queries.GetFiltered;

internal sealed class GetFilteredQuery : IQuery
{
	private readonly UsersQueryRepository _repository;
	private readonly JwtVerificationObject _verification;
	private readonly int _page;
	private readonly int _pageSize;
	private readonly IRepositoryExpression<User> _expression;

	public IQueryHandler<GetFilteredQuery, IReadOnlyCollection<UserResponse>> Handler;

	public GetFilteredQuery(
		string? name,
		string? surname,
		string? thirdname,
		string? email,
		string token,
		int page,
		int pageSize
	)
	{
		_verification = new JwtVerificationObject(token);
		_page = page;
		_pageSize = pageSize;
		_repository = new UsersQueryRepository();
		_expression = ExpressionsFactory.Filter(name, surname, thirdname, email);
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(UsersQueryRepository repository) : IQueryHandler<GetFilteredQuery, IReadOnlyCollection<UserResponse>>
	{
		private readonly UsersQueryRepository _repository = repository;
		private readonly UserVerificationService _service = new UserVerificationService();

		public async Task<OperationResult<IReadOnlyCollection<UserResponse>>> Handle(GetFilteredQuery query)
		{
			if (query._verification.IsExpired)
				return new OperationResult<IReadOnlyCollection<UserResponse>>(new UserSessionExpiredError().ToString());

			User? user = await _repository.GetById(query._verification.UserId);
			if (user == null)
				return new OperationResult<IReadOnlyCollection<UserResponse>>(new UserNotFoundError().ToString());

			Result verification = _service.VerifyRole(user, User.Admin);
			if (verification.IsFailure)
				return new OperationResult<IReadOnlyCollection<UserResponse>>(verification.Error);

			user.UpdateLoginDate();
			await _repository.Commit();

			IReadOnlyCollection<User> users = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<UserResponse>>(users.Select(u => new UserResponse(u)).ToList());
		}
	}
}
