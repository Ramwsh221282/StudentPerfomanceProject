using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.All;

public sealed class GetAllEducationPlansQuery(IRepository<EducationPlan> repository) : IQuery
{
	public readonly IQueryHandler<GetAllEducationPlansQuery, IReadOnlyCollection<EducationPlan>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationPlan> repository) : IQueryHandler<GetAllEducationPlansQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly IRepository<EducationPlan> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetAllEducationPlansQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
