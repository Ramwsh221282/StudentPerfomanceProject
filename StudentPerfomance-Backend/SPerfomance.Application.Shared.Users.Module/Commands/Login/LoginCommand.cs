using SPerfomance.Application.Shared.Module.Authentication;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.DTOs;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

namespace SPerfomance.Application.Shared.Users.Module.Commands.Login;

public class LoginCommand : ICommand
{
	private readonly UserLoginDTO _dto;
	private readonly IRepositoryExpression<User> _getUser;
	private readonly UsersQueryRepository _repository;

	public readonly ICommandHandler<LoginCommand, AuthenticatedUser> Handler;

	public LoginCommand(UserLoginDTO dto)
	{
		_dto = dto;
		UserRepositoryObject parameter = new UserRepositoryObject()
		.WithEmail(_dto.Email.CreateValueOrEmpty());
		_getUser = ExpressionsFactory.GetByEmail(parameter);
		_repository = new UsersQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(UsersQueryRepository repository) : ICommandHandler<LoginCommand, AuthenticatedUser>
	{
		private readonly UsersQueryRepository _repository = repository;
		private readonly PasswordHasher _hasher = new PasswordHasher();
		private readonly JwtProvider _provider = new JwtProvider();

		public async Task<OperationResult<AuthenticatedUser>> Handle(LoginCommand command)
		{
			User? user = await _repository.GetByParameter(command._getUser);
			if (user == null)
				return new OperationResult<AuthenticatedUser>(new UserNotFoundError().ToString());

			string password = command._dto.Password.CreateValueOrEmpty();
			if (string.IsNullOrWhiteSpace(password))
				return new OperationResult<AuthenticatedUser>(new PasswordEmptyError().ToString());

			bool verification = _hasher.Verify(password, user.HashedPassword);
			if (!verification)
				return new OperationResult<AuthenticatedUser>(new PasswordEmptyError().ToString());

			string token = _provider.GenerateToken(user);
			AuthenticatedUser authenticatedUser = new AuthenticatedUser(token, user);
			return new OperationResult<AuthenticatedUser>(authenticatedUser);
		}
	}
}
