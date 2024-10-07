using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly TeacherQueryRepository _repository;
	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<Teacher>> Handler;

	public GetPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherQueryRepository repository) : IQueryHandler<GetPagedQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetPagedQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}
