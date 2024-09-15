using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.DeleteTeacher;

internal sealed class DeleteTeacherCommandHandler
(
	IRepository<Teacher> repository
)
: ICommandHandler<DeleteTeacherCommand, OperationResult<Teacher>>
{
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<Teacher>> Handle(DeleteTeacherCommand command)
	{
		if (!command.Validator.IsValid)
			return new OperationResult<Teacher>(command.Validator.Error);
		Teacher? teacher = await _repository.GetByParameter(command.Expression);
		if (teacher == null)
			return new OperationResult<Teacher>("Преподаватель не найден");
		await _repository.Remove(teacher);
		return new OperationResult<Teacher>(teacher);
	}
}
