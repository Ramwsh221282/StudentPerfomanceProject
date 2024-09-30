using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Create;

internal sealed class CreateEducationDirectionCommandHandler
(
	IRepository<EducationDirection> repository
) : ICommandHandler<CreateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> Handle(CreateEducationDirectionCommand command)
	{
		if (!command.Validator.IsValid)
			return new OperationResult<EducationDirection>(command.Validator.Error);
		if (await _repository.HasEqualRecord(command.CodeUniqueness))
			return new OperationResult<EducationDirection>(new EducationDirectionCodeDublicateError(command.Schema.Code).ToString());
		EducationDirection direction = command.Schema.CreateDomainObject();
		await _repository.Create(direction);
		return new OperationResult<EducationDirection>(direction);
	}
}
