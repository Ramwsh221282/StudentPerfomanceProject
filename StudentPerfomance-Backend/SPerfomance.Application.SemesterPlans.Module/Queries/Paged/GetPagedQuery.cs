using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly SemesterPlansQueryRepository _repository;

	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<SemesterPlan>> Handler;

	public GetPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterPlansQueryRepository repository) : IQueryHandler<GetPagedQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(GetPagedQuery query)
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}
