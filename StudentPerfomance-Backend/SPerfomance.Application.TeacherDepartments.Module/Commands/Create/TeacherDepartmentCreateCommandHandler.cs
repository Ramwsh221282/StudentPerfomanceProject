using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Create;

internal sealed class TeacherDepartmentCreateCommandHandler
(
	IRepository<TeachersDepartment> repository
) : ICommandHandler<TeacherDepartmentCreateCommand, OperationResult<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentCreateCommand command)
	{
		if (!command.Validator.IsValid)
			return new OperationResult<TeachersDepartment>(command.Validator.Error);
		if (await _repository.HasEqualRecord(command.NameDublicate))
			return new OperationResult<TeachersDepartment>(new DepartmentNameDublicateError(command.Department.FullName).ToString());
		TeachersDepartment department = command.Department.CreateDomainObject();
		await _repository.Create(department);
		return new OperationResult<TeachersDepartment>(department);
	}
}
