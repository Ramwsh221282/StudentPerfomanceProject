using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update;

public sealed class UpdateEducationDirectionCommand : ICommand
{
	private readonly EducationDirectionSchema _newSchema;
	private readonly IRepositoryExpression<EducationDirection> _findDirection;
	private readonly IRepositoryExpression<EducationDirection> _checkForCodeDublicate;
	private readonly ISchemaValidator _schemaValidator;
	public readonly ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> Handler;
	public UpdateEducationDirectionCommand
	(
		EducationDirectionSchema newSchema,
		IRepositoryExpression<EducationDirection> findDirection,
		IRepositoryExpression<EducationDirection> checkForDublicate,
		IRepository<EducationDirection> repository
	)
	{
		_newSchema = newSchema;
		_findDirection = findDirection;
		_checkForCodeDublicate = checkForDublicate;
		_schemaValidator = new EducationDirectionValidator()
		.WithNameValidation(newSchema)
		.WithCodeValidator(newSchema)
		.WithTypeValidation(newSchema);
		_schemaValidator.ProcessValidation();
		Handler = new UpdateEducationDirectionDecoratorBuilder(repository).Build();
	}

	internal abstract class CommandHandler
	(
		ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> handler
	) : ICommandHandler<UpdateEducationDirectionCommand, EducationDirection>
	{
		private readonly ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> _handler = handler;
		public async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command) => await _handler.Handle(command);
	}

	internal class UpdateEducationDirectionDecorator
	(
		ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> handler
	) : ICommandHandler<UpdateEducationDirectionCommand, EducationDirection>
	{
		private readonly ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> _handler = handler;
		public virtual async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command) => await _handler.Handle(command);
	}

	internal sealed class UpdateEducationDirectionDefaultHandler
	(
		IRepository<EducationDirection> repository
	) : ICommandHandler<UpdateEducationDirectionCommand, EducationDirection>
	{
		private readonly IRepository<EducationDirection> _repository = repository;
		public async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
		{
			EducationDirection? direction = await _repository.GetByParameter(command._findDirection);
			if (direction == null) return new OperationResult<EducationDirection>(new EducationDirectionNotFoundError().ToString());
			return new OperationResult<EducationDirection>(direction);
		}
	}

	internal sealed class UpdateEducationDirectionNameHandler : UpdateEducationDirectionDecorator
	{
		public UpdateEducationDirectionNameHandler(ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> handler)
		: base(handler) { }

		public override async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Name.Name == command._newSchema.Name) return result;
			result.Result.ChangeDirectionName(command._newSchema.CreateDirectionName());
			return new OperationResult<EducationDirection>(result.Result);
		}
	}

	internal sealed class UpdateEducationDirectionCodeHandler : UpdateEducationDirectionDecorator
	{
		private readonly IRepository<EducationDirection> _repository;
		public UpdateEducationDirectionCodeHandler
		(
			ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> handler,
			IRepository<EducationDirection> repository
		)
		: base(handler) => _repository = repository;

		public override async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Code.Code == command._newSchema.Code) return result;
			if (await _repository.HasEqualRecord(command._checkForCodeDublicate))
				return new OperationResult<EducationDirection>(new EducationDirectionCodeDublicateError(command._newSchema.Code).ToString());
			result.Result.ChangeDirectionCode(command._newSchema.CreateDirectionCode());
			return new OperationResult<EducationDirection>(result.Result);
		}
	}

	internal sealed class UpdateEducationDirectionDecoratorBuilder
	{
		private readonly IRepository<EducationDirection> _repository;
		private ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> _handler;
		public UpdateEducationDirectionDecoratorBuilder(IRepository<EducationDirection> repository)
		{
			_repository = repository;
			_handler = new UpdateEducationDirectionDefaultHandler(repository);
		}
		public ICommandHandler<UpdateEducationDirectionCommand, EducationDirection> Build()
		{
			BuildWithCodeChange();
			BuildWithNameChange();
			return _handler;
		}
		private void BuildWithCodeChange() => _handler = new UpdateEducationDirectionCodeHandler(_handler, _repository);
		private void BuildWithNameChange() => _handler = new UpdateEducationDirectionNameHandler(_handler);
	}
}
