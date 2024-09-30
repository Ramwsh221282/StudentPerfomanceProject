using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update.Decorators;

internal sealed class UpdateEducationDirectionDefaultHandler
(
	IRepository<EducationDirection> repository
) : ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		EducationDirection? direction = await _repository.GetByParameter(command.FindDirection);
		if (direction == null) return new OperationResult<EducationDirection>(new EducationDirectionNotFoundError().ToString());
		return new OperationResult<EducationDirection>(direction);
	}
}
