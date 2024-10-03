using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Delete;

public sealed class DeleteEducationDirectionCommand(IRepositoryExpression<EducationDirection> findDirection, IRepository<EducationDirection> repository) : ICommand
{
	private readonly IRepositoryExpression<EducationDirection> _findDirection = findDirection;
	public readonly ICommandHandler<DeleteEducationDirectionCommand, EducationDirection> Handler = new CommandHandler(repository);

	internal sealed class CommandHandler(IRepository<EducationDirection> repository) : ICommandHandler<DeleteEducationDirectionCommand, EducationDirection>
	{
		private readonly IRepository<EducationDirection> _repository = repository;
		public async Task<OperationResult<EducationDirection>> Handle(DeleteEducationDirectionCommand command)
		{
			EducationDirection? direction = await _repository.GetByParameter(command._findDirection);
			if (direction == null)
				return new OperationResult<EducationDirection>(new EducationDirectionNotFoundError().ToString());
			await _repository.Remove(direction);
			return new OperationResult<EducationDirection>(direction);
		}
	}
}
