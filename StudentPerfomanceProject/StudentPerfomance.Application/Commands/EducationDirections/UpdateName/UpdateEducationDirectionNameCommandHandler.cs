using StudentPerfomance.Application.EntitySchemas.Validators;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.EducationDirections;

namespace StudentPerfomance.Application.Commands.EducationDirections.UpdateName;

internal sealed class UpdateEducationDirectionNameCommandHandler
(
	IRepository<EducationDirection> repository
) : CommandWithErrorBuilder, ICommandHandler<UpdateEducationDirectionNameCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionNameCommand command)
	{
		command.Validator.ValidateSchema(this);
		EducationDirection? direction = await _repository.GetByParameter(command.FindDirection);
		direction.ValidateNullability("Не найдено направление подготовки для изменения названия", this);
		return await this.ProcessAsync(async () =>
		{
			DirectionName name = command.Direction.CreateDirectionName();
			direction.ChangeDirectionName(name);
			await _repository.Commit();
			return new OperationResult<EducationDirection>(direction);
		});
	}
}
