using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly EducationPlanQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<EducationPlan>> Handler;
	public GetAllQuery()
	{
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(EducationPlanQueryRepository repository) : IQueryHandler<GetAllQuery, IReadOnlyCollection<EducationPlan>>
	{
		private readonly EducationPlanQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationPlan>>> Handle(GetAllQuery query)
		{
			IReadOnlyCollection<EducationPlan> plans = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<EducationPlan>>(plans);
		}
	}
}
