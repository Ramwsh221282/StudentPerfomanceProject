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

namespace SPerfomance.Application.Shared.Users.Module.Commands.UpdatePassword;

internal sealed class UpdatePasswordCommand : ICommand
{
	private readonly UserLoginDTO _userData;
	private readonly IRepositoryExpression<User> _getUser;
	private readonly UsersQueryRepository _repository;
	private readonly string _newPassword;

	public readonly ICommandHandler<UpdatePasswordCommand, User> Handler;

	public UpdatePasswordCommand(UserLoginDTO userdata, string newPassword)
	{
		_userData = userdata;
		_newPassword = newPassword;
		string email = _userData.Email.CreateValueOrEmpty();
		UserRepositoryObject parameter = new UserRepositoryObject().WithEmail(email);
		_getUser = ExpressionsFactory.GetByEmail(parameter);
		_repository = new UsersQueryRepository();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(UsersQueryRepository repository) : ICommandHandler<UpdatePasswordCommand, User>
	{
		private readonly UsersQueryRepository _repository = repository;
		private readonly PasswordHasher _hasher = new PasswordHasher();

		public async Task<OperationResult<User>> Handle(UpdatePasswordCommand command)
		{
			User? user = await _repository.GetByParameter(command._getUser);
			if (user == null)
				return new OperationResult<User>(new UserNotFoundError().ToString());

			string currentPassword = command._userData.Password.CreateValueOrEmpty();
			bool verification = _hasher.Verify(currentPassword, user.HashedPassword);
			if (!verification)
				return new OperationResult<User>(new PasswordNotValidError().ToString());

			string updatedHashedPassword = _hasher.Generate(command._newPassword);
			user.UpdatePassword(updatedHashedPassword);
			await _repository.Commit();
			return new OperationResult<User>(user);
		}
	}
}
