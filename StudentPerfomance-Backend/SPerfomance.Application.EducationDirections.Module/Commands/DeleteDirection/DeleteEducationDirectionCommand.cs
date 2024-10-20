using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.DTOs.EducationDirections;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Users.Module.Commands.Common;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationDirections.Module.Commands.DeleteDirection;

internal sealed class DeleteEducationDirectionCommand : ICommand
{
	private readonly IRepositoryExpression<EducationDirection> _expression;
	private readonly EducationDirectionsCommandRepository _repository;
	public ICommandHandler<DeleteEducationDirectionCommand, EducationDirection> Handler;

	public DeleteEducationDirectionCommand(EducationDirectionDTO direction, string token)
	{
		_expression = ExpressionsFactory.GetDirection(direction.ToSchema().ToRepositoryObject());
		_repository = new EducationDirectionsCommandRepository();
		Handler = new VerificationHandler<DeleteEducationDirectionCommand, EducationDirection>(token, User.Admin);
		Handler = new CommandHandler(Handler, _repository);
	}

	internal abstract class HandlerDecorator
	(ICommandHandler<DeleteEducationDirectionCommand, EducationDirection> handler)
	 : ICommandHandler<DeleteEducationDirectionCommand, EducationDirection>
	{
		private readonly ICommandHandler<DeleteEducationDirectionCommand, EducationDirection> _handler = handler;

		public virtual async Task<OperationResult<EducationDirection>> Handle(DeleteEducationDirectionCommand command) =>
			await _handler.Handle(command);
	}

	internal sealed class CommandHandler : HandlerDecorator
	{
		private readonly EducationDirectionsCommandRepository _repository;

		public CommandHandler(ICommandHandler<DeleteEducationDirectionCommand, EducationDirection> handler, EducationDirectionsCommandRepository repository)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<EducationDirection>> Handle(DeleteEducationDirectionCommand command)
		{
			OperationResult<EducationDirection> result = await base.Handle(command);
			if (result.IsFailed)
				return result;

			Result<EducationDirection> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<EducationDirection>(delete.Error) :
				new OperationResult<EducationDirection>(delete.Value);
		}
	}
}
