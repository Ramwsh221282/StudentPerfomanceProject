using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.PagedFilters;

public sealed class FilterEducationPlansQuery(int page, int pageSize, IRepositoryExpression<EducationPlan> expression, IRepository<EducationPlan> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<EducationPlan> _expression = expression;
	public readonly IQueryHandler<FilterEducationPlansQuery, IReadOnlyCollection<EducationPlan>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationPlan> repository) : IQueryHandler<FilterEducationPlansQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly IRepository<EducationPlan> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(FilterEducationPlansQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
