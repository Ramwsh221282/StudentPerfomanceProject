using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByPage;

public sealed class TeachersPaginationService
(
	int page,
	int pageSize,
	IRepositoryExpression<Teacher> expression,
	IRepository<Teacher> repository
)
: IService<IReadOnlyCollection<Teacher>>
{
	private readonly GetTeachersByPageQuery _query = new GetTeachersByPageQuery(page, pageSize, expression);
	private readonly GetTeachersByPageQueryHandler _handler = new GetTeachersByPageQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation() => await _handler.Handle(_query);
}
