using CSharpFunctionalExtensions;

using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Commands;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.DeleteDirection;

internal sealed class DeleteEducationDirectionCommand : ICommand
{
	private readonly IRepositoryExpression<EducationDirection> _expression;
	private readonly EducationDirectionsCommandRepository _repository;
	public ICommandHandler<DeleteEducationDirectionCommand, EducationDirection> Handler;
	public DeleteEducationDirectionCommand(EducationDirectionSchema direction)
	{
		_expression = ExpressionsFactory.GetDirection(direction.ToRepositoryObject());
		_repository = new EducationDirectionsCommandRepository();
		Handler = new CommandHandler(_repository);
	}
	internal sealed class CommandHandler(EducationDirectionsCommandRepository repository) : ICommandHandler<DeleteEducationDirectionCommand, EducationDirection>
	{
		private readonly EducationDirectionsCommandRepository _repository = repository;
		public async Task<OperationResult<EducationDirection>> Handle(DeleteEducationDirectionCommand command)
		{
			Result<EducationDirection> delete = await _repository.Remove(command._expression);
			return delete.IsFailure ?
				new OperationResult<EducationDirection>(delete.Error) :
				new OperationResult<EducationDirection>(delete.Value);
		}
	}
}
