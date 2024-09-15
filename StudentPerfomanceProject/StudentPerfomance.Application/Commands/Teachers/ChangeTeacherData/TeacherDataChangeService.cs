using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.ChangeTeacherData;

public sealed class TeacherDataChangeService
(
	TeacherSchema schema,
	IRepositoryExpression<Teacher> existance,
	IRepositoryExpression<Teacher> dublicate,
	IRepository<Teacher> repository
)
: IService<Teacher>
{
	private readonly ChangeTeacherDataCommand _command = new ChangeTeacherDataCommand(schema, existance, dublicate);
	private readonly ChangeTeacherDataCommandHandler _handler = new ChangeTeacherDataCommandHandler(repository);
	public async Task<OperationResult<Teacher>> DoOperation() => await _handler.Handle(_command);
}
