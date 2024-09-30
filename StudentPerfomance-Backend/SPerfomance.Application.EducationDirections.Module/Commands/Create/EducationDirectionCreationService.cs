using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Create;

public sealed class EducationDirectionCreationService
(
	EducationDirectionSchema schema,
	IRepositoryExpression<EducationDirection> codeUniqueness,
	IRepository<EducationDirection> repository
) : IService<EducationDirection>
{
	private readonly CreateEducationDirectionCommand _command = new CreateEducationDirectionCommand(schema, codeUniqueness);
	private readonly CreateEducationDirectionCommandHandler _handler = new CreateEducationDirectionCommandHandler(repository);
	public async Task<OperationResult<EducationDirection>> DoOperation() => await _handler.Handle(_command);
}
