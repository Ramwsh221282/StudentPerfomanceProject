using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.DeleteSemester;

public sealed class SemesterDeletionService
(
	IRepositoryExpression<Semester> existance,
	IRepository<Semester> repository
)
: IService<Semester>
{
	private readonly DeleteSemesterCommand _command = new DeleteSemesterCommand(existance);
	private readonly DeleteSemesterCommandHandler _handler = new DeleteSemesterCommandHandler(repository);
	public async Task<OperationResult<Semester>> DoOperation() => await _handler.Handle(_command);
}
