using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.SharedServices.Auth;
using SPerfomance.Application.Shared.Module.SharedServices.Auth.Errors;
using SPerfomance.Application.Shared.Module.SharedServices.Mailing;
using SPerfomance.Application.Shared.Module.SharedServices.Mailing.MailingMessages;
using SPerfomance.Application.Shared.Users.Module.API.Responses;
using SPerfomance.Application.Shared.Users.Module.DTOs;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;

namespace SPerfomance.Application.Shared.Users.Module.Commands.Remove;

internal sealed class RemoveUserCommand : ICommand
{
	private readonly UserActionDTO _userData;
	private readonly IRepositoryExpression<User> _getUser;
	private readonly UsersCommandRepository _commandRepository;
	private readonly UsersQueryRepository _queryRepository;
	private readonly JwtVerificationObject _verification;

	public ICommandHandler<RemoveUserCommand, UserResponse> Handler;

	public RemoveUserCommand(UserActionDTO userData, string token)
	{
		_userData = userData;
		UserRepositoryObject parameter = new UserRepositoryObject()
		.WithName(_userData.Name.CreateValueOrEmpty())
		.WithSurname(_userData.Surname.CreateValueOrEmpty())
		.WithThirdname(_userData.Thirdname.CreateValueOrEmpty())
		.WithEmail(_userData.Email.CreateValueOrEmpty());
		_getUser = ExpressionsFactory.GetUser(parameter);
		_commandRepository = new UsersCommandRepository();
		_queryRepository = new UsersQueryRepository();
		_verification = new JwtVerificationObject(token);
		Handler = new VerificationHandler(_queryRepository);
		Handler = new RemoveHandler(Handler, _queryRepository, _commandRepository);
		Handler = new MailingHandler(Handler);
	}

	internal abstract class RemoveDecorator(ICommandHandler<RemoveUserCommand, UserResponse> handler)
	 : ICommandHandler<RemoveUserCommand, UserResponse>
	{
		private readonly ICommandHandler<RemoveUserCommand, UserResponse> _handler = handler;

		public virtual async Task<OperationResult<UserResponse>> Handle(RemoveUserCommand command) => await _handler.Handle(command);
	}

	internal sealed class VerificationHandler(UsersQueryRepository repository) : ICommandHandler<RemoveUserCommand, UserResponse>
	{
		private readonly UsersQueryRepository _repository = repository;
		private readonly UserVerificationService _service = new UserVerificationService();

		public async Task<OperationResult<UserResponse>> Handle(RemoveUserCommand command)
		{
			if (command._verification.IsExpired)
				return new OperationResult<UserResponse>(new UserSessionExpiredError().ToString());

			User? invoker = await _repository.GetById(command._verification.UserId);

			if (invoker == null)
				return new OperationResult<UserResponse>(new UserNotFoundError().ToString());

			Result roleVerification = _service.VerifyRole(invoker, User.Admin);
			if (roleVerification.IsFailure)
				return new OperationResult<UserResponse>(roleVerification.Error);

			invoker.UpdateLoginDate();
			await _repository.Commit();

			return new OperationResult<UserResponse>().SetNotFailedFlag();
		}
	}

	internal sealed class RemoveHandler : RemoveDecorator
	{
		private readonly UsersQueryRepository _queryRepository;
		private readonly UsersCommandRepository _commandRepository;

		public RemoveHandler(ICommandHandler<RemoveUserCommand, UserResponse> handler, UsersQueryRepository queryRepository, UsersCommandRepository commandRepository)
		 : base(handler)
		{
			_queryRepository = queryRepository;
			_commandRepository = commandRepository;
		}

		public override async Task<OperationResult<UserResponse>> Handle(RemoveUserCommand command)
		{
			OperationResult<UserResponse> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			User? user = await _queryRepository.GetByParameter(command._getUser);
			if (user == null)
				return new OperationResult<UserResponse>(new UserNotFoundError().ToString());

			Result<User> remove = await _commandRepository.Remove(user);
			if (remove.IsFailure)
				return new OperationResult<UserResponse>(remove.Error);

			return new OperationResult<UserResponse>(new UserResponse(user));
		}
	}

	internal sealed class MailingHandler : RemoveDecorator
	{
		private readonly MailingService _service = new MailingService();

		public MailingHandler(ICommandHandler<RemoveUserCommand, UserResponse> handler) : base(handler) { }

		public override async Task<OperationResult<UserResponse>> Handle(RemoveUserCommand command)
		{
			OperationResult<UserResponse> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			MailingMessage message = new UserRemoveMessage(result.Result.Email);
			var sending = _service.SendMessage(message);

			return result;
		}
	}
}
