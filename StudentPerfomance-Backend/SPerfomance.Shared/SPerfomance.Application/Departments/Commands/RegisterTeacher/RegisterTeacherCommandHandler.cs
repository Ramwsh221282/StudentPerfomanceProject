using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.RegisterTeacher;

public class RegisterTeacherCommandHandler
(
	ITeachersRepository repository
) : ICommandHandler<RegisterTeacherCommand, Teacher>
{
	private readonly ITeachersRepository _repository = repository;

	public async Task<Result<Teacher>> Handle(RegisterTeacherCommand command)
	{
		if (command.Department == null)
			return Result<Teacher>.Failure(TeacherDepartmentErrors.NotFound());

		Result<Teacher> registeredTeacher = command.Department.RegisterTeacher(
			command.Name,
			command.Surname,
			command.Patronymic,
			command.WorkingState,
			command.JobTitle);

		if (registeredTeacher.IsFailure)
			return Result<Teacher>.Failure(registeredTeacher.Error);

		await _repository.Insert(registeredTeacher.Value);
		return Result<Teacher>.Success(registeredTeacher.Value);
	}
}
