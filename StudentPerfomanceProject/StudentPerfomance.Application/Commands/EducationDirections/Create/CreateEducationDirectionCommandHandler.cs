using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Create;

internal sealed class CreateEducationDirectionCommandHandler
(
	IRepository<EducationDirection> repository
) : CommandWithErrorBuilder, ICommandHandler<CreateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> Handle(CreateEducationDirectionCommand command)
	{
		command.Validator.ValidateSchema(this);
		await _repository.ValidateExistance(command.Dublicate, "Невозможно создать дубликат направления подготовки", this);
		return await this.ProcessAsync(async () =>
		{
			EducationDirection direction = command.Schema.CreateDomainObject();
			await _repository.Create(direction);
			return new OperationResult<EducationDirection>(direction);
		});
	}
}
