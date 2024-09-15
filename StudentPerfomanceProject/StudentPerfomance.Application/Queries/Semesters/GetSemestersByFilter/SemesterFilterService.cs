using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Semesters.GetSemestersByFilter;

public sealed class SemesterFilterService
(
	int page,
	int pageSize,
	IRepositoryExpression<Semester> expression,
	IRepository<Semester> repository
)
: IService<IReadOnlyCollection<Semester>>
{
	private readonly GetSemestersByFilterQuery _query = new GetSemestersByFilterQuery(page, pageSize, expression);
	private readonly GetSemestersByFilterQueryHandler _handler = new GetSemestersByFilterQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<Semester>>> DoOperation() => await _handler.Handle(_query);
}
