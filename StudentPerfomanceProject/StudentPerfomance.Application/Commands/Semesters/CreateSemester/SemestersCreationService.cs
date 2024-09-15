using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.CreateSemester;

public sealed class SemestersCreationService
(
	SemesterSchema schema,
	IRepositoryExpression<Semester> dublicate,
	IRepositoryExpression<StudentGroup> existance,
	IRepository<Semester> semesters,
	IRepository<StudentGroup> groups
)
: IService<Semester>
{
	private readonly CreateSemesterCommand _command = new CreateSemesterCommand(schema, dublicate, existance);
	private readonly CreateSemesterCommandHandler _handler = new CreateSemesterCommandHandler(semesters, groups);
	public async Task<OperationResult<Semester>> DoOperation() => await _handler.Handle(_command);
}
