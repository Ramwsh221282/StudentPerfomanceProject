using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Departments.CreateDepartment;

public sealed class DepartmentCreationService
(
	DepartmentSchema schema,
	IRepository<TeachersDepartment> repository,
	IRepositoryExpression<TeachersDepartment> expression
)
: IService<TeachersDepartment>
{
	private readonly CreateDepartmentCommand _command = new CreateDepartmentCommand(schema);
	private readonly CreateDepartmentCommandHandler _handler = new CreateDepartmentCommandHandler(repository, expression);
	public async Task<OperationResult<TeachersDepartment>> DoOperation() => await _handler.Handle(_command);
}
