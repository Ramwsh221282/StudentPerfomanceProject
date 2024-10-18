using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Users;
using SPerfomance.Application.Shared.Users.Module.DTOs;
using SPerfomance.Application.Shared.Users.Module.Repositories;
using SPerfomance.Application.Shared.Users.Module.Services;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Users.Module.Commands.Create;

internal sealed class CreateUserCommand : ICommand
{
	private readonly UserRegistrationDTO _dto;
	private readonly ICommandHandler<CreateUserCommand, UserSchema> SchemaCommandHandler;
	private string _generatedPassword;

	public readonly ICommandHandler<CreateUserCommand, (User, string)> Handler;

	public CreateUserCommand(UserRegistrationDTO Dto)
	{
		_generatedPassword = string.Empty;
		_dto = Dto;
		SchemaCommandHandler = new DefaultSchemaHandler();
		SchemaCommandHandler = new UserSchemaRoleHandler(SchemaCommandHandler);
		SchemaCommandHandler = new UserSchemaPasswordGenerator(SchemaCommandHandler);
		Handler = new UserRegistrationHandler(SchemaCommandHandler);
	}

	internal sealed class DefaultSchemaHandler() : ICommandHandler<CreateUserCommand, UserSchema>
	{
		public async Task<OperationResult<UserSchema>> Handle(CreateUserCommand command)
		{
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

	internal abstract class UserSchemaHandlerDecorator(ICommandHandler<CreateUserCommand, UserSchema> handler) : ICommandHandler<CreateUserCommand, UserSchema>
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

	internal sealed class UserRegistrationHandler(
		ICommandHandler<CreateUserCommand, UserSchema> schemaHandler
	) : ICommandHandler<CreateUserCommand, (User, string)>
	{
		private readonly UsersCommandRepository _repository = new UsersCommandRepository();
		private readonly ICommandHandler<CreateUserCommand, UserSchema> _schemaHandler = schemaHandler;

		public async Task<OperationResult<(User, string)>> Handle(CreateUserCommand command)
		{
			OperationResult<UserSchema> result = await _schemaHandler.Handle(command);
			if (result.Result == null || result.IsFailed)
				return new OperationResult<(User, string)>(result.Error);

			User user = result.Result.CreateDomainObject().Value;
			Result<User> create = await _repository.Create(user);
			return create.IsFailure ?
				new OperationResult<(User, string)>(create.Error) :
				new OperationResult<(User, string)>((create.Value, command._generatedPassword));
		}
	}
}
