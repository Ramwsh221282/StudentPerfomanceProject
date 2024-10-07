using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.EducationPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<EducationPlan> _expression;
	private readonly EducationPlanQueryRepository _repository;
	public IQueryHandler<SearchQuery, IReadOnlyCollection<EducationPlan>> Handler;
	public SearchQuery(EducationPlanSchema plan)
	{
		_expression = ExpressionsFactory.Filter(plan.ToRepositoryObject());
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(EducationPlanQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
