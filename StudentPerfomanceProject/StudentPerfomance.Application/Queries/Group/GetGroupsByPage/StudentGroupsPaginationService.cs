using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Group.GetGroupsByPage;

public sealed class StudentGroupsPaginationService
(
	int page,
	int pageSize,
	IRepository<StudentGroup> repository
)
: IService<IReadOnlyCollection<StudentGroup>>
{
	private readonly GetGroupsByPageQuery _query = new GetGroupsByPageQuery(page, pageSize);
	private readonly GetGroupsByPageQueryHandler _handler = new GetGroupsByPageQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> DoOperation() => await _handler.Handle(_query);
}
