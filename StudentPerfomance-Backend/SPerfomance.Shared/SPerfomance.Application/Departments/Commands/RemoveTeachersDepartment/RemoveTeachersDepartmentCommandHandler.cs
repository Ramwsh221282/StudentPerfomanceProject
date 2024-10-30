using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.RemoveTeachersDepartment;

public class RemoveTeachersDepartmentCommandHandler
(
	ITeacherDepartmentsRepository repository
)
: ICommandHandler<RemoveTeachersDepartmentCommand, TeachersDepartments>
{
	private readonly ITeacherDepartmentsRepository _repository = repository;

	public async Task<Result<TeachersDepartments>> Handle(RemoveTeachersDepartmentCommand command)
	{
		if (command.Department == null)
			return Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.NotFound());

		await _repository.Remove(command.Department);
		return Result<TeachersDepartments>.Success(command.Department);
	}
}
