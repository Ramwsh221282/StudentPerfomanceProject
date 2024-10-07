using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.CreateDirection;

internal sealed class CreateEducationDirectionCommand : ICommand
{
	private readonly EducationDirectionSchema _direction;
	private readonly IRepositoryExpression<EducationDirection> _findDublicate;
	private readonly EducationDirectionsCommandRepository _repository = new EducationDirectionsCommandRepository();
	private readonly ISchemaValidator _validator;
	public readonly ICommandHandler<CreateEducationDirectionCommand, EducationDirection> Handler;
	public CreateEducationDirectionCommand(EducationDirectionSchema direction)
	{
		_direction = direction;
		_findDublicate = ExpressionsFactory.GetByCode(direction.ToRepositoryObject());
		_validator = new EducationDirectionValidator()
		.WithNameValidation(direction)
		.WithCodeValidator(direction)
		.WithTypeValidation(direction);
		_validator.ProcessValidation();
		Handler = new CommandHandler(_repository);
	}

	internal sealed class CommandHandler(EducationDirectionsCommandRepository repository) : ICommandHandler<CreateEducationDirectionCommand, EducationDirection>
	{
		private readonly EducationDirectionsCommandRepository _repository = repository;

		public async Task<OperationResult<EducationDirection>> Handle(CreateEducationDirectionCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<EducationDirection>(command._validator.Error);
			Result<EducationDirection> create = await _repository.Create(command._direction, command._findDublicate);
			return create.IsFailure ?
				new OperationResult<EducationDirection>(create.Error) :
				new OperationResult<EducationDirection>(create.Value);
		}
	}
}
