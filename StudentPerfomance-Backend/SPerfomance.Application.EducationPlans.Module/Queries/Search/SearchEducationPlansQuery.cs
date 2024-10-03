using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Search;

public sealed class SearchEducationPlansQuery(IRepositoryExpression<EducationPlan> expression, IRepository<EducationPlan> repository) : IQuery
{
	private readonly IRepositoryExpression<EducationPlan> _expression = expression;
	public readonly IQueryHandler<SearchEducationPlansQuery, IReadOnlyCollection<EducationPlan>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationPlan> repository) : IQueryHandler<SearchEducationPlansQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly IRepository<EducationPlan> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(SearchEducationPlansQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
