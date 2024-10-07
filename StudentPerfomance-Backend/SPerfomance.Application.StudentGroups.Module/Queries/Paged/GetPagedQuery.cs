using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly StudentGroupQueryRepository _repository;
	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<StudentGroup>> Handler;
	public GetPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentGroupQueryRepository repository) : IQueryHandler<GetPagedQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetPagedQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
