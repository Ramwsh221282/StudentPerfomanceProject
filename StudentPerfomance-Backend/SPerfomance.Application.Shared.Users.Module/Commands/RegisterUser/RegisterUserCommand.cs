using System.Text;

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
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Users.Module.Commands.RegisterUser;

internal sealed class RegisterUserCommand : ICommand
{
	private readonly UserActionDTO _userData;
	private readonly UsersQueryRepository _queryRepository;
	private readonly UsersCommandRepository _commandRepository;
	private readonly JwtVerificationObject _verification;
	private string assignedPassword;

	public readonly ICommandHandler<RegisterUserCommand, UserResponse> Handler;

	public RegisterUserCommand(UserActionDTO userData, string token)
	{
		_userData = userData;
		_verification = new JwtVerificationObject(token);
		_queryRepository = new UsersQueryRepository();
		_commandRepository = new UsersCommandRepository();
		assignedPassword = string.Empty;
		Handler = new VerificationHandler(_queryRepository);
		Handler = new RegistrationHandler(Handler, _commandRepository);
		Handler = new MailingHandler(Handler);
	}

	internal abstract class RegistrationDecorator(ICommandHandler<RegisterUserCommand, UserResponse> handler)
	: ICommandHandler<RegisterUserCommand, UserResponse>
	{
		private readonly ICommandHandler<RegisterUserCommand, UserResponse> _handler = handler;

		public virtual async Task<OperationResult<UserResponse>> Handle(RegisterUserCommand command) =>
			await _handler.Handle(command);
	}

	internal sealed class VerificationHandler(UsersQueryRepository repository) : ICommandHandler<RegisterUserCommand, UserResponse>
	{
		private readonly UsersQueryRepository _repository = repository;

		public async Task<OperationResult<UserResponse>> Handle(RegisterUserCommand command)
		{
			if (command._verification.IsExpired)
				return new OperationResult<UserResponse>(new UserSessionExpiredError().ToString());

			User? invoker = await _repository.GetById(command._verification.UserId);
			if (invoker == null)
				return new OperationResult<UserResponse>(new UserNotFoundError().ToString());

			invoker.UpdateLoginDate();
			await _repository.Commit();

			return new OperationResult<UserResponse>().SetNotFailedFlag();
		}
	}

	internal sealed class RegistrationHandler : RegistrationDecorator
	{
		private readonly UsersCommandRepository _repository;
		private readonly PasswordHasher _hasher;
		private readonly PasswordGenerator _generator;

		public RegistrationHandler(ICommandHandler<RegisterUserCommand, UserResponse> handler, UsersCommandRepository repository)
		 : base(handler)
		{
			_repository = repository;
			_hasher = new PasswordHasher();
			_generator = new PasswordGenerator();
		}

		public override async Task<OperationResult<UserResponse>> Handle(RegisterUserCommand command)
		{
			OperationResult<UserResponse> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			string email = command._userData.Email.CreateValueOrEmpty();
			Result<EmailValueObject> emailCreation = EmailValueObject.Create(email);
			if (emailCreation.IsFailure)
				return new OperationResult<UserResponse>(emailCreation.Error);

			string name = command._userData.Name.CreateValueOrEmpty();
			string surname = command._userData.Surname.CreateValueOrEmpty();
			string thirdname = command._userData.Thirdname.CreateValueOrEmpty();
			Result<Username> nameCreation = Username.Create(name, surname, thirdname);
			if (nameCreation.IsFailure)
				return new OperationResult<UserResponse>(nameCreation.Error);

			string role = command._userData.Role.CreateValueOrEmpty();
			Result<UserRole> roleCreation = UserRole.Create(role);
			if (roleCreation.IsFailure)
				return new OperationResult<UserResponse>(roleCreation.Error);

			string generatedPassword = _generator.Generate();
			string hashedPassword = _hasher.Generate(generatedPassword);
			Result<User> userCreation = User.Create(emailCreation.Value, nameCreation.Value, roleCreation.Value, hashedPassword);
			Result<User> databaseCreation = await _repository.Create(userCreation.Value);
			if (databaseCreation.IsFailure)
				return new OperationResult<UserResponse>(databaseCreation.Error);

			command.assignedPassword = generatedPassword;
			return new OperationResult<UserResponse>(new UserResponse(databaseCreation.Value));
		}
	}

	internal sealed class MailingHandler : RegistrationDecorator
	{
		private readonly MailingService _service;

		public MailingHandler(ICommandHandler<RegisterUserCommand, UserResponse> handler) : base(handler)
		{
			_service = new MailingService();
		}

		public override async Task<OperationResult<UserResponse>> Handle(RegisterUserCommand command)
		{
			OperationResult<UserResponse> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			StringBuilder content = new StringBuilder();
			content.AppendLine($"Почта: {result.Result.Email}:");
			content.AppendLine($"Пароль: {command.assignedPassword}");

			MailingMessage message = new UserRegistrationMessage(result.Result.Email, content.ToString());
			Task<Result> sending = _service.SendMessage(message);

			return result;
		}
	}
}
