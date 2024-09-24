using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Create;

public sealed class CreateEducationDirectionService
(
	EducationDirectionSchema schema,
	IRepositoryExpression<EducationDirection> dublicate,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly CreateEducationDirectionCommand _command = new CreateEducationDirectionCommand(schema, dublicate);
	private readonly CreateEducationDirectionCommandHandler _handler = new CreateEducationDirectionCommandHandler(repository);
	public async Task<OperationResult<EducationDirection>> DoOperation() => await _handler.Handle(_command);
}
