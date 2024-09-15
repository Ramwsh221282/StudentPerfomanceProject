using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Departments.DeleteDepartment;

public sealed class DepartmentDeletionService
(
	DepartmentSchema schema,
	IRepository<TeachersDepartment> repository,
	IRepositoryExpression<TeachersDepartment> expression
)
: IService<TeachersDepartment>
{
	private readonly DeleteDepartmentCommand _command = new DeleteDepartmentCommand(schema);
	private readonly DeleteDepartmentCommandHandler _handler = new DeleteDepartmentCommandHandler(repository, expression);
	public async Task<OperationResult<TeachersDepartment>> DoOperation() => await _handler.Handle(_command);
}
