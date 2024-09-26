using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update.Decorators;

internal sealed class UpdateEducationDirectionDefaultHandler
(
	IRepository<EducationDirection> repository
) : ICommandHandler<UpdateEducationDirectionCommand, OperationResult<EducationDirection>>
{
	private readonly IRepository<EducationDirection> _repository = repository;
	public async Task<OperationResult<EducationDirection>> Handle(UpdateEducationDirectionCommand command)
	{
		EducationDirection? direction = await _repository.GetByParameter(command.FindDirection);
		if (direction == null) return new OperationResult<EducationDirection>("Направление подготовки не найдено");
		return new OperationResult<EducationDirection>(direction);
	}
}
