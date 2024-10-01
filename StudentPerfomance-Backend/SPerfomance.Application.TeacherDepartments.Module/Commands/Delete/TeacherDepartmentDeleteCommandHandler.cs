using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments.Errors;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Delete;

internal sealed class TeacherDepartmentDeleteCommandHandler
(
	IRepository<TeachersDepartment> repository
) : ICommandHandler<TeacherDepartmentDeleteCommand, OperationResult<TeachersDepartment>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<TeachersDepartment>> Handle(TeacherDepartmentDeleteCommand command)
	{
		TeachersDepartment? department = await _repository.GetByParameter(command.Expression);
		if (department == null)
			return new OperationResult<TeachersDepartment>(new DepartmentNotFountError().ToString());
		await _repository.Remove(department);
		return new OperationResult<TeachersDepartment>(department);
	}
}
