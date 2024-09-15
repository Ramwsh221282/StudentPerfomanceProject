using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Departments.ChangeDepartmentName;

public sealed class DepartmentChangeNameService
(
	DepartmentSchema schema,
	IRepository<TeachersDepartment> repository,
	IRepositoryExpression<TeachersDepartment> existance,
	IRepositoryExpression<TeachersDepartment> dublicate
)
: IService<TeachersDepartment>
{
	private readonly ChangeDepartmentNameCommand _command = new ChangeDepartmentNameCommand(schema);
	private readonly ChangeDepartmentNameCommandHandler _handler = new ChangeDepartmentNameCommandHandler(repository, existance, dublicate);
	public async Task<OperationResult<TeachersDepartment>> DoOperation() => await _handler.Handle(_command);
}
