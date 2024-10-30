using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;

public class ChangeTeachersDepartmentNameCommandHandler
(
	ITeacherDepartmentsRepository repository
)
 : ICommandHandler<ChangeTeachersDepartmentNameCommand, TeachersDepartments>
{
	private readonly ITeacherDepartmentsRepository _repository = repository;

	public async Task<Result<TeachersDepartments>> Handle(ChangeTeachersDepartmentNameCommand command)
	{
		if (command.Department == null)
			return Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.NotFound());

		string name = command.NewName.ValueOrEmpty();
		if (await _repository.HasWithName(name))
			return Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.DepartmentDublicate(name));

		Result<TeachersDepartments> department = command.Department.ChangeName(command.NewName);
		if (department.IsFailure)
			return department;

		await _repository.Update(department.Value);
		return department;
	}
}
