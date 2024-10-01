using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Create;

public sealed class TeacherDepartmentCreationService
(
	DepartmentSchema department,
	IRepositoryExpression<TeachersDepartment> nameDublicate,
	IRepository<TeachersDepartment> repository
) : IService<TeachersDepartment>
{
	private readonly TeacherDepartmentCreateCommand _command = new TeacherDepartmentCreateCommand(department, nameDublicate);
	private readonly TeacherDepartmentCreateCommandHandler _handler = new TeacherDepartmentCreateCommandHandler(repository);
	public async Task<OperationResult<TeachersDepartment>> DoOperation() => await _handler.Handle(_command);
}
