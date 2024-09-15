using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.CreateTeacher;

public sealed class TeacherCreationService
(
	TeacherSchema schema,
	IRepositoryExpression<Teacher> dublicate,
	IRepositoryExpression<TeachersDepartment> existance,
	IRepository<Teacher> teachers,
	IRepository<TeachersDepartment> departments
)
: IService<Teacher>
{
	private readonly CreateTeacherCommand _command = new CreateTeacherCommand(schema, dublicate, existance);
	private readonly CreateTeacherCommandHandler _handler = new CreateTeacherCommandHandler(teachers, departments);
	public async Task<OperationResult<Teacher>> DoOperation() => await _handler.Handle(_command);
}
