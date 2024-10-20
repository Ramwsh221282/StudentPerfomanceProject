using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationDirections.Module.Commands.CreateDirection;

internal sealed class CreateEducationDirectionCommand : ICommand
{
	private readonly EducationDirectionSchema _direction;
	private readonly IRepositoryExpression<EducationDirection> _findDublicate;
	private readonly EducationDirectionsCommandRepository _repository = new EducationDirectionsCommandRepository();
	private readonly ISchemaValidator _validator;


	public readonly ICommandHandler<CreateEducationDirectionCommand, EducationDirection> Handler;

	public CreateEducationDirectionCommand(EducationDirectionDTO direction, string token)
	{
		_direction = direction.ToSchema();
		_findDublicate = ExpressionsFactory.GetByCode(_direction.ToRepositoryObject());
		_validator = new EducationDirectionValidator()
		.WithNameValidation(_direction)
		.WithCodeValidator(_direction)
		.WithTypeValidation(_direction);
		_validator.ProcessValidation();
		Handler = new VerificationHandler<CreateEducationDirectionCommand, EducationDirection>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal abstract class HandlerDecorator(ICommandHandler<CreateEducationDirectionCommand, EducationDirection> handler)
	: ICommandHandler<CreateEducationDirectionCommand, EducationDirection>
	{
		private readonly ICommandHandler<CreateEducationDirectionCommand, EducationDirection> _handler = handler;

		public virtual async Task<OperationResult<EducationDirection>> Handle(CreateEducationDirectionCommand command) => await _handler.Handle(command);
	}

	internal sealed class CommandHandler : HandlerDecorator
	{
		private readonly EducationDirectionsCommandRepository _repository;

		public CommandHandler(ICommandHandler<CreateEducationDirectionCommand, EducationDirection> handler, EducationDirectionsCommandRepository repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<EducationDirection>> Handle(CreateEducationDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			if (!command._validator.IsValid) return new OperationResult<EducationDirection>(command._validator.Error);
			Result<EducationDirection> create = await _repository.Create(command._direction, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<EducationDirection>(create.Error) :
				new OperationResult<EducationDirection>(create.Value);
		}
	}
}
