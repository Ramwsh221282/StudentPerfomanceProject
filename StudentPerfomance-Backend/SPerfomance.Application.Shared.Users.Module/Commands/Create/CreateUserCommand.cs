using System.Text;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Users;
using SPerfomance.Application.Shared.Module.SharedServices.Auth;
using SPerfomance.Application.Shared.Module.SharedServices.Mailing;
using SPerfomance.Application.Shared.Module.SharedServices.Mailing.MailingMessages;
using SPerfomance.Application.Shared.Users.Module.DTOs;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Users.Module.Commands.Create;

internal sealed class CreateUserCommand : ICommand
{
	private readonly UserRegistrationDTO _dto;
	private readonly ICommandHandler<CreateUserCommand, UserSchema> SchemaCommandHandler;
	private readonly IRepositoryExpression<User> _getInvoker;
	private readonly UsersQueryRepository _repository;
	private string _generatedPassword;

	public readonly ICommandHandler<CreateUserCommand, User> Handler;

	public CreateUserCommand(UserRegistrationDTO Dto, JwtVerificationObject jwtObject)
	{
		_getInvoker = ExpressionsFactory.GetById(jwtObject.UserId);
		_generatedPassword = string.Empty;
		_dto = Dto;
		_repository = new UsersQueryRepository();
		SchemaCommandHandler = new DefaultSchemaHandler(_repository);
		SchemaCommandHandler = new UserSchemaRoleHandler(SchemaCommandHandler);
		SchemaCommandHandler = new UserSchemaPasswordGenerator(SchemaCommandHandler);
		Handler = new UserRegistrationHandler(SchemaCommandHandler, _repository);
		Handler = new MailingHandler(Handler);
	}

	internal sealed class DefaultSchemaHandler(UsersQueryRepository repository) : ICommandHandler<CreateUserCommand, UserSchema>
	{
		private readonly UserVerificationService _service = new UserVerificationService();
		private readonly UsersQueryRepository _repository = repository;

		public async Task<OperationResult<UserSchema>> Handle(CreateUserCommand command)
		{
			User? user = await _repository.GetByParameter(command._getInvoker);
			if (user == null)
				return new OperationResult<UserSchema>(new UserNotFoundError().ToString());

			Result verificationAdmin = _service.VerifyRole(user, User.Admin);
			Result verificationAdvancedTeacher = _service.VerifyRole(user, User.AdvancedTeacher);
			if (verificationAdmin.IsFailure && verificationAdvancedTeacher.IsFailure)
				return new OperationResult<UserSchema>(verificationAdmin.Error);

			user.UpdateLoginDate();

			string name = command._dto.Name.CreateValueOrEmpty();
			string surname = command._dto.Surname.CreateValueOrEmpty();
			string thirdname = command._dto.Thirdname.CreateValueOrEmpty();
			string email = command._dto.Email.CreateValueOrEmpty();

			Result<Username> nameRequest = Username.Create(name, surname, thirdname);
			if (nameRequest.IsFailure)
				return new OperationResult<UserSchema>(nameRequest.Error);

			Result<EmailValueObject> emailRequest = EmailValueObject.Create(email);
			if (emailRequest.IsFailure)
				return new OperationResult<UserSchema>(emailRequest.Error);

			UserSchema result = new UserSchema(email, name, surname, thirdname);
			return await Task.FromResult(new OperationResult<UserSchema>(result));
		}
	}

	internal abstract class UserSchemaHandlerDecorator(ICommandHandler<CreateUserCommand, UserSchema> handler)
	: ICommandHandler<CreateUserCommand, UserSchema>
	{
		private readonly ICommandHandler<CreateUserCommand, UserSchema> _handler = handler;

		public virtual async Task<OperationResult<UserSchema>> Handle(CreateUserCommand command) => await _handler.Handle(command);
	}

	internal sealed class UserSchemaPasswordGenerator : UserSchemaHandlerDecorator
	{
		private readonly PasswordGenerator _generator = new PasswordGenerator();
		private readonly PasswordHasher _hasher = new PasswordHasher();

		public UserSchemaPasswordGenerator(ICommandHandler<CreateUserCommand, UserSchema> handler) : base(handler) { }

		public override async Task<OperationResult<UserSchema>> Handle(CreateUserCommand command)
		{
			OperationResult<UserSchema> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			command._generatedPassword = _generator.Generate();
			string hashed = _hasher.Generate(command._generatedPassword);
			result.Result.HashedPassword = hashed;
			return result;
		}
	}

	internal sealed class UserSchemaRoleHandler : UserSchemaHandlerDecorator
	{
		public UserSchemaRoleHandler(ICommandHandler<CreateUserCommand, UserSchema> handler) : base(handler) { }

		public override async Task<OperationResult<UserSchema>> Handle(CreateUserCommand command)
		{
			OperationResult<UserSchema> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			string role = command._dto.Role.CreateValueOrEmpty();
			Result<UserRole> request = UserRole.Create(role);
			if (request.IsFailure)
				return new OperationResult<UserSchema>(request.Error);

			result.Result.Role = request.Value.Value;
			return result;
		}
	}

	internal abstract class UserRegistrationDecorator : ICommandHandler<CreateUserCommand, User>
	{
		private readonly ICommandHandler<CreateUserCommand, User> _handler;

		public UserRegistrationDecorator(ICommandHandler<CreateUserCommand, User> handler)
		{
			_handler = handler;
		}

		public virtual async Task<OperationResult<User>> Handle(CreateUserCommand command)
		{
			return await _handler.Handle(command);
		}
	}

	internal sealed class UserRegistrationHandler(
		ICommandHandler<CreateUserCommand, UserSchema> schemaHandler, UsersQueryRepository queryRepository
	) : ICommandHandler<CreateUserCommand, User>
	{
		private readonly UsersQueryRepository _queryRepository = queryRepository;
		private readonly UsersCommandRepository _repository = new UsersCommandRepository();
		private readonly ICommandHandler<CreateUserCommand, UserSchema> _schemaHandler = schemaHandler;

		public async Task<OperationResult<User>> Handle(CreateUserCommand command)
		{
			OperationResult<UserSchema> result = await _schemaHandler.Handle(command);
			if (result.Result == null || result.IsFailed)
				return new OperationResult<User>(result.Error);

			User user = result.Result.CreateDomainObject().Value;
			Result<User> create = await _repository.Create(user);

			if (create.IsFailure)
				new OperationResult<User>(create.Error);

			await _queryRepository.Commit();
			return new OperationResult<User>(create.Value);
		}
	}

	internal sealed class MailingHandler : UserRegistrationDecorator
	{
		private readonly MailingService _service = new MailingService();

		public MailingHandler(ICommandHandler<CreateUserCommand, User> handler) : base(handler) { }

		public override async Task<OperationResult<User>> Handle(CreateUserCommand command)
		{
			OperationResult<User> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed)
				return result;

			if (string.IsNullOrWhiteSpace(command._generatedPassword))
				return new OperationResult<User>(new PasswordEmptyError().ToString());

			StringBuilder content = new StringBuilder();
			content.AppendLine("Вы были зарегистрированы на платформе SPerfomance");
			content.AppendLine("Ваши данные для входа: ");
			content.AppendLine($"Почта: {result.Result.Email.Email}");
			content.AppendLine($"Пароль: {command._generatedPassword}");

			MailingMessage message = new UserRegistrationMessage(result.Result.Email.Email, content.ToString());
			Result mailing = await _service.SendMessage(message);
			if (mailing.IsFailure)
				return new OperationResult<User>(mailing.Error);

			return result;
		}
	}
}
