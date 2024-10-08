using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.Searched;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<SemesterPlan> _expression;
	private readonly SemesterPlansQueryRepository _repository;

	public IQueryHandler<SearchQuery, IReadOnlyCollection<SemesterPlan>> Handler;

	public SearchQuery(SemesterPlanSchema schema)
	{
		_expression = ExpressionsFactory.Filter(schema.ToRepositoryObject());
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterPlansQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}
