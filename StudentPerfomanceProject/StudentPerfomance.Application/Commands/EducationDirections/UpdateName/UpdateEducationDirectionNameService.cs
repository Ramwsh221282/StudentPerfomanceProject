using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.UpdateName;

public sealed class UpdateEducationDirectionNameService
(
	EducationDirectionSchema direction,
	IRepositoryExpression<EducationDirection> findDirection,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly UpdateEducationDirectionNameCommand _command = new UpdateEducationDirectionNameCommand(direction, findDirection);
	private readonly UpdateEducationDirectionNameCommandHandler _handler = new UpdateEducationDirectionNameCommandHandler(repository);
	public async Task<OperationResult<EducationDirection>> DoOperation() => await _handler.Handle(_command);
}
