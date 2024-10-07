using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.GetPaged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly EducationPlanQueryRepository _repository;
	public IQueryHandler<GetPagedQuery, IReadOnlyCollection<EducationPlan>> Handler;
	public GetPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(EducationPlanQueryRepository repository) : IQueryHandler<GetPagedQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetPagedQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
