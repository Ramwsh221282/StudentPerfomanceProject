using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Count;

public sealed class GetEducationPlansCountQuery(IRepository<EducationPlan> repository) : IQuery
{
	public readonly IQueryHandler<GetEducationPlansCountQuery, int> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationPlan> repository) : IQueryHandler<GetEducationPlansCountQuery, int>
	{
		private readonly IRepository<EducationPlan> _repository = repository;
		public async Task<OperationResult<int>> Handle(GetEducationPlansCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
