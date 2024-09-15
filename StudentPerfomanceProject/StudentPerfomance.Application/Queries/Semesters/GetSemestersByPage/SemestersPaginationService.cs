using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Semesters.GetSemestersByPage;

public sealed class SemestersPaginationService
(
	int page,
	int pageSize,
	IRepository<Semester> repository
)
: IService<IReadOnlyCollection<Semester>>
{
	private readonly GetSemestersByPageQuery _query = new GetSemestersByPageQuery(page, pageSize);
	private readonly GetSemestersByPageQueryHandler _handler = new GetSemestersByPageQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<Semester>>> DoOperation() => await _handler.Handle(_query);
}
