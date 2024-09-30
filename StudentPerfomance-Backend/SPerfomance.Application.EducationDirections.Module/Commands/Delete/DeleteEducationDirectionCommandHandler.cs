using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Delete;

internal sealed class DeleteEducationDirectionCommandHandler
(
	IRepository<EducationDirection> repository
)
: ICommandHandler<DeleteEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;

	public async Task<OperationResult<EducationDirection>> Handle(DeleteEducationDirectionCommand command)
	{
		EducationDirection? direction = await _repository.GetByParameter(command.FindDirection);
		if (direction == null) return new OperationResult<EducationDirection>(new EducationDirectionNotFoundError().ToString());
		await _repository.Remove(direction);
		return new OperationResult<EducationDirection>(direction);
	}
}
