using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.TeacherDepartments.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;

public class CreateTeachersDepartmentCommandHandler
(
	ITeacherDepartmentsRepository repository
) : ICommandHandler<CreateTeachersDepartmentCommand, TeachersDepartments>
{
	private readonly ITeacherDepartmentsRepository _repository = repository;

	public async Task<Result<TeachersDepartments>> Handle(CreateTeachersDepartmentCommand command)
	{
		Result<TeachersDepartments> creation = TeachersDepartments.Create(command.Name);
		if (creation.IsFailure)
			return creation;

		if (await _repository.HasWithName(command.Name))
			return Result<TeachersDepartments>.Failure(TeacherDepartmentErrors.DepartmentDublicate(command.Name));

		await _repository.Insert(creation.Value);
		return Result<TeachersDepartments>.Success(creation.Value);
	}
}
