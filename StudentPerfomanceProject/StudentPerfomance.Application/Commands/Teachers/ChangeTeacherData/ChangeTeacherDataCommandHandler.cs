using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Application.Commands.Teachers.ChangeTeacherData;

internal sealed class ChangeTeacherDataCommandHandler
(
	IRepository<Teacher> repository
)
: ICommandHandler<ChangeTeacherDataCommand, OperationResult<Teacher>>
{
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<Teacher>> Handle(ChangeTeacherDataCommand command)
	{
		if (await _repository.HasEqualRecord(command.Dublicate))
			return new OperationResult<Teacher>("Преподаватель с таким именем уже существует");
		Teacher? teacher = await _repository.GetByParameter(command.Existance);
		if (teacher == null)
			return new OperationResult<Teacher>("Преподаватель не найден");
		TeacherName name = TeacherName.Create(command.Teacher.Name, command.Teacher.Surname, command.Teacher.Thirdname).Value;
		teacher.ChangeName(name);
		await _repository.Commit();
		return new OperationResult<Teacher>(teacher);
	}
}
