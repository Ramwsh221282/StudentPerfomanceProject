using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.UpdateDirection;

internal sealed class UpdateDirectionCommand : ICommand
{
	private readonly EducationDirectionSchema _newDirection;
	private readonly ISchemaValidator _validator;
	private readonly IRepositoryExpression<EducationDirection> _getInitial;
	private readonly IRepositoryExpression<EducationDirection> _findDublicate;
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly ICommandHandler<UpdateDirectionCommand, EducationDirection> Handler;
	public UpdateDirectionCommand(EducationDirectionSchema oldSchema, EducationDirectionSchema newSchema)
	{
		_getInitial = ExpressionsFactory.GetDirection(oldSchema.ToRepositoryObject());
		_findDublicate = ExpressionsFactory.GetByCode(newSchema.ToRepositoryObject());
		_newDirection = newSchema;
		_validator = new EducationDirectionValidator()
		.WithNameValidation(newSchema)
		.WithCodeValidator(newSchema)
		.WithTypeValidation(newSchema);
		_validator.ProcessValidation();
		_repository = new EducationDirectionsQueryRepository();
		Handler = new DefaultUpdateHandler(_repository);
		Handler = new CodeUpdateHandler(Handler, _repository);
		Handler = new NameUpdateHandler(Handler);
		Handler = new CommitHandler(Handler, _repository);
	}

	internal sealed class DefaultUpdateHandler(EducationDirectionsQueryRepository repository) : ICommandHandler<UpdateDirectionCommand, EducationDirection>
	{
		private readonly EducationDirectionsQueryRepository _repository = repository;

		public async Task<OperationResult<EducationDirection>> Handle(UpdateDirectionCommand command)
		{
			if (!command._validator.IsValid) return new OperationResult<EducationDirection>(command._validator.Error);
			EducationDirection? direction = await _repository.GetByParameter(command._getInitial);
			return direction == null ?
				new OperationResult<EducationDirection>(new EducationDirectionNotFoundError().ToString()) :
				new OperationResult<EducationDirection>(direction);
		}
	}

	internal abstract class HandlerDecorator(ICommandHandler<UpdateDirectionCommand, EducationDirection> handler) : ICommandHandler<UpdateDirectionCommand, EducationDirection>
	{
		private readonly ICommandHandler<UpdateDirectionCommand, EducationDirection> _handler = handler;
		public virtual async Task<OperationResult<EducationDirection>> Handle(UpdateDirectionCommand command) => await _handler.Handle(command);
	}

	internal sealed class CodeUpdateHandler : HandlerDecorator
	{
		private readonly EducationDirectionsQueryRepository _repository;
		public CodeUpdateHandler(ICommandHandler<UpdateDirectionCommand, EducationDirection> handler, EducationDirectionsQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public async override Task<OperationResult<EducationDirection>> Handle(UpdateDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Code.Code != command._newDirection.Code)
			{
				if (await _repository.HasEqualRecord(command._findDublicate))
					return new OperationResult<EducationDirection>(new EducationDirectionCodeDublicateError(command._newDirection.Code).ToString());
				result.Result.ChangeDirectionCode(command._newDirection.CreateDirectionCode());
			}
			return result;
		}
	}

	internal sealed class NameUpdateHandler : HandlerDecorator
	{
		public NameUpdateHandler(ICommandHandler<UpdateDirectionCommand, EducationDirection> handler) : base(handler) { }
		public override async Task<OperationResult<EducationDirection>> Handle(UpdateDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			if (result.Result.Name.Name != command._newDirection.Name)
				result.Result.ChangeDirectionName(command._newDirection.CreateDirectionName());
			return result;
		}
	}

	internal sealed class CommitHandler : HandlerDecorator
	{
		private readonly EducationDirectionsQueryRepository _repository;
		public CommitHandler(ICommandHandler<UpdateDirectionCommand, EducationDirection> handler, EducationDirectionsQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<EducationDirection>> Handle(UpdateDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.Result == null || result.IsFailed) return result;
			await _repository.Commit();
			return result;
		}
	}
}
