using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Delete;

internal sealed class DeleteEducationDirectionCommandHandler
(
	IRepository<EducationDirection> repository
)
: CommandWithErrorBuilder, ICommandHandler<DeleteEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;

	public async Task<OperationResult<EducationDirection>> Handle(DeleteEducationDirectionCommand command)
	{
		EducationDirection? direction = await _repository.GetByParameter(command.FindDirection);
		direction.ValidateNullability("Направление подготовки не найдено. Транзакция отклонена", this);
		return await this.ProcessAsync(async () =>
		{
			await _repository.Remove(direction);
			return new OperationResult<EducationDirection>(direction);
		});
	}
}
