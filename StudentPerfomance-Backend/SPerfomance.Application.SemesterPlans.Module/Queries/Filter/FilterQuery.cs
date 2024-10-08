using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly SemesterPlansQueryRepository _repository;
	private readonly int _page;
	private readonly int _pageSize;
	private readonly IRepositoryExpression<SemesterPlan> _expression;

	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<SemesterPlan>> Handler;

	public FilterQuery(SemesterPlanSchema schema, int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new SemesterPlansQueryRepository();
		_expression = ExpressionsFactory.Filter(schema.ToRepositoryObject());
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterPlansQueryRepository repository) : IQueryHandler<FilterQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(FilterQuery query)
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}
