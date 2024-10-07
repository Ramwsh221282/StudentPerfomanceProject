using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.GetFiltered;

internal sealed class GetFilteredQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly IRepositoryExpression<EducationPlan> _expression;
	private readonly EducationPlanQueryRepository _repository;
	public readonly IQueryHandler<GetFilteredQuery, IReadOnlyCollection<EducationPlan>> Handler;
	public GetFilteredQuery(EducationPlanSchema plan, int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_expression = ExpressionsFactory.Filter(plan.ToRepositoryObject());
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(EducationPlanQueryRepository repository) : IQueryHandler<GetFilteredQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetFilteredQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
