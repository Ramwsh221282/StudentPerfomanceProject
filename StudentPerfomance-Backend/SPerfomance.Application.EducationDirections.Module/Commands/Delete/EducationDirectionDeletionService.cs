using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Delete;

public sealed class EducationDirectionDeletionService
(
	IRepositoryExpression<EducationDirection> findDirection,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly DeleteEducationDirectionCommand _command = new DeleteEducationDirectionCommand(findDirection);
	private readonly DeleteEducationDirectionCommandHandler _handler = new DeleteEducationDirectionCommandHandler(repository);
	public async Task<OperationResult<EducationDirection>> DoOperation() => await _handler.Handle(_command);
}
