using SPerfomance.Application.Shared.Module.Schemas.Departments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Services;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Commands.Update;

public sealed class TeachersDepartmentUpdateService
(
	DepartmentSchema newSchema,
	IRepositoryExpression<TeachersDepartment> findInitial,
	IRepositoryExpression<TeachersDepartment> findNameDublicate,
	IRepository<TeachersDepartment> repository
) : IService<TeachersDepartment>
{
	private readonly TeachersDepartmentUpdateCommand _command = new TeachersDepartmentUpdateCommand(newSchema, findInitial, findNameDublicate);
	private readonly TeachersDepartmentUpdateCommandHandler _handler = new TeachersDepartmentUpdateCommandHandler(repository);
	public async Task<OperationResult<TeachersDepartment>> DoOperation() => await _handler.Handle(_command);
}
