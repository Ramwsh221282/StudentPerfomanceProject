using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByFilter;

public sealed class TeachersFilterService
(
	int page,
	int pageSize,
	IRepositoryExpression<Teacher> expression,
	IRepository<Teacher> repository
)
: IService<IReadOnlyCollection<Teacher>>
{
	private readonly GetTeachersByFilterQuery _query = new GetTeachersByFilterQuery(page, pageSize, expression);
	private readonly GetTeachersByFilterQueryHandler _handler = new GetTeachersByFilterQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation() => await _handler.Handle(_query);
}
