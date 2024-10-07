using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Semester.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly SemesterQueryRepository _repository;
	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> Handler;
	public GetPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new SemesterQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterQueryRepository repository) : IQueryHandler<GetPagedQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>
	{
		private readonly SemesterQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>> Handle(GetPagedQuery query)
		{
			IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester> semesters = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(semesters);
		}
	}
}
