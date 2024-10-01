using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Update;

internal sealed class TeachersDepartmentUpdateCommandHandler
(
	IRepository<TeachersDepartment> repository
) : ICommandHandler<TeachersDepartmentUpdateCommand, OperationResult<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<TeachersDepartment>> Handle(TeachersDepartmentUpdateCommand command)
	{
		TeachersDepartment? department = await _repository.GetByParameter(command.FindInitial);
		if (department == null)
			return new OperationResult<TeachersDepartment>(new DepartmentNotFountError().ToString());
		if (await _repository.HasEqualRecord(command.FindNameDublicate))
			return new OperationResult<TeachersDepartment>(new DepartmentNameDublicateError(command.NewSchema.FullName).ToString());
		department.ChangeDepartmentName(command.NewSchema.FullName);
		await _repository.Commit();
		return new OperationResult<TeachersDepartment>(department);
	}
}
