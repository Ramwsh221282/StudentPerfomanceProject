using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;
using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Application.Commands.Teachers.CreateTeacher;

internal sealed class CreateTeacherCommandHandler
(
	IRepository<Teacher> teachersRepository,
	IRepository<TeachersDepartment> departmentsRepository
)
: ICommandHandler<CreateTeacherCommand, OperationResult<Teacher>>
{
	private readonly IRepository<Teacher> _teachersRepository = teachersRepository;
	private readonly IRepository<TeachersDepartment> _departmentsRepository = departmentsRepository;
	public async Task<OperationResult<Teacher>> Handle(CreateTeacherCommand command)
	{
		if (!command.Validator.IsValid)
			return new OperationResult<Teacher>(command.Validator.Error);
		if (await _teachersRepository.HasEqualRecord(command.Dublicate))
			return new OperationResult<Teacher>("Преподаватель уже существует");
		TeachersDepartment? department = await _departmentsRepository.GetByParameter(command.Existance);
		if (department == null)
			return new OperationResult<Teacher>("Несуществующая кафедра");
		TeacherName name = TeacherName.Create(command.Teacher.Name, command.Teacher.Surname, command.Teacher.Thirdname).Value;
		Teacher teacher = Teacher.Create(Guid.NewGuid(), name, department).Value;
		await _teachersRepository.Create(teacher);
		return new OperationResult<Teacher>(teacher);
	}
}
