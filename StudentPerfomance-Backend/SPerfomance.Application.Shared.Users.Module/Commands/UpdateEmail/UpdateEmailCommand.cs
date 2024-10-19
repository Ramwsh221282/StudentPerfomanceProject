using CSharpFunctionalExtensions;

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
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Users.Module.Commands.UpdateEmail;

internal sealed class UpdateEmailCommand : ICommand
{
	private readonly UserLoginDTO _userData;
	private readonly UsersQueryRepository _repository;
	private readonly IRepositoryExpression<User> _getUser;
	private readonly IRepositoryExpression<User> _findDublicate;
	private readonly string _newEmail;

	public readonly ICommandHandler<UpdateEmailCommand, User> Handler;

	public UpdateEmailCommand(UserLoginDTO userData, string newEmail)
	{
		_newEmail = newEmail;
		_userData = userData;
		string email = _userData.Email.CreateValueOrEmpty();
		UserRepositoryObject parameter = new UserRepositoryObject().WithEmail(email);
		UserRepositoryObject dublicate = new UserRepositoryObject().WithEmail(newEmail);
		_getUser = ExpressionsFactory.GetByEmail(parameter);
		_findDublicate = ExpressionsFactory.GetByEmail(dublicate);
		_repository = new UsersQueryRepository();
		Handler = new ValidationHandler();
		Handler = new FindUserHandler(Handler, _repository);
		Handler = new VerifyUserHandler(Handler);
		Handler = new UpdateEmailHandler(Handler, _repository);
	}

	internal sealed class ValidationHandler : ICommandHandler<UpdateEmailCommand, User>
	{
		public async Task<OperationResult<User>> Handle(UpdateEmailCommand command)
		{
			Result<EmailValueObject> request = EmailValueObject.Create(command._newEmail);
			if (request.IsFailure)
				return new OperationResult<User>(request.Error);
			return await Task.FromResult(new OperationResult<User>().SetNotFailedFlag());
		}
	}

	internal abstract class HandlerDecorator(ICommandHandler<UpdateEmailCommand, User> handler)
	: ICommandHandler<UpdateEmailCommand, User>
	{
		private readonly ICommandHandler<UpdateEmailCommand, User> _handler = handler;

		public virtual async Task<OperationResult<User>> Handle(UpdateEmailCommand command) => await _handler.Handle(command);
	}

	internal sealed class FindUserHandler : HandlerDecorator
	{
		private readonly UsersQueryRepository _repository;

		public FindUserHandler(ICommandHandler<UpdateEmailCommand, User> handler, UsersQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<User>> Handle(UpdateEmailCommand command)
		{
			// Подразумевается Validation Handler result
			OperationResult<User> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Task<User?> getUser = _repository.GetByParameter(command._getUser);
			Task<bool> getDublicate = _repository.HasEqualRecord(command._findDublicate);

			await Task.WhenAll(getUser, getDublicate);

			if (getDublicate.Result == true)
				return new OperationResult<User>(new EmailIsNotFreeError(command._newEmail).ToString());

			if (getUser.Result == null)
				return new OperationResult<User>(new UserNotFoundError().ToString());

			return new OperationResult<User>(getUser.Result);
		}
	}

	internal sealed class VerifyUserHandler : HandlerDecorator
	{
		private readonly PasswordHasher _hasher = new PasswordHasher();

		public VerifyUserHandler(ICommandHandler<UpdateEmailCommand, User> handler) : base(handler) { }

		public override async Task<OperationResult<User>> Handle(UpdateEmailCommand command)
		{
			// Подразуемевается Find User Handler result
			OperationResult<User> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			string password = command._userData.Password.CreateValueOrEmpty();
			bool verification = _hasher.Verify(password, result.Result.HashedPassword);
			return verification == true ?
				result :
				new OperationResult<User>(new PasswordNotValidError().ToString());
		}
	}

	internal sealed class UpdateEmailHandler : HandlerDecorator
	{
		private readonly UsersQueryRepository _repository;

		public UpdateEmailHandler(ICommandHandler<UpdateEmailCommand, User> handler, UsersQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<User>> Handle(UpdateEmailCommand command)
		{
			OperationResult<User> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			Result<EmailValueObject> updatedEmail = EmailValueObject.Create(command._newEmail);
			if (updatedEmail.IsFailure)
				return new OperationResult<User>(updatedEmail.Error);

			result.Result.Email.ChangeEmail(updatedEmail.Value);
			await _repository.Commit();
			return result;
		}
	}
}
