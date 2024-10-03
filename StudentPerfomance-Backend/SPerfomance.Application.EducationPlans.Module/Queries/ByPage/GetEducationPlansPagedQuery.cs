using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.ByPage;

public sealed class GetEducationPlansPagedQuery(int page, int pageSize, IRepository<EducationPlan> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public readonly IQueryHandler<GetEducationPlansPagedQuery, IReadOnlyCollection<EducationPlan>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationPlan> repository) : IQueryHandler<GetEducationPlansPagedQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly IRepository<EducationPlan> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetEducationPlansPagedQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
