using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Delete;

public sealed class DeleteEducationDirectionService
(
	IRepositoryExpression<EducationDirection> findDirection,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly DeleteEducationDirectionCommand _command = new DeleteEducationDirectionCommand(findDirection);
	private readonly DeleteEducationDirectionCommandHandler _handler = new DeleteEducationDirectionCommandHandler(repository);
	public async Task<OperationResult<EducationDirection>> DoOperation() => await _handler.Handle(_command);
}
