using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.DeleteTeacher;

public sealed class TeachersDeletionService
(
	TeacherSchema schema,
	IRepository<Teacher> repository,
	IRepositoryExpression<Teacher> expression
)
: IService<Teacher>
{
	private readonly DeleteTeacherCommand _command = new DeleteTeacherCommand(schema, expression);
	private readonly DeleteTeacherCommandHandler _handler = new DeleteTeacherCommandHandler(repository);
	public async Task<OperationResult<Teacher>> DoOperation() => await _handler.Handle(_command);
}
