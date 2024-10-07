using SPerfomance.Application.EducationPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.EducationPlans.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly EducationPlanQueryRepository _repository;
	public readonly IQueryHandler<GetCountQuery, int> Handler;
	public GetCountQuery()
	{
		_repository = new EducationPlanQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(EducationPlanQueryRepository repository) : IQueryHandler<GetCountQuery, int>
	{
		private readonly EducationPlanQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
