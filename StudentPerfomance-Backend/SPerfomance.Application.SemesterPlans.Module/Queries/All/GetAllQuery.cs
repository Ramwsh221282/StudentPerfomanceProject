using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly SemesterPlansQueryRepository _repository;
	public IQueryHandler<GetAllQuery, IReadOnlyCollection<SemesterPlan>> Handler;
	public GetAllQuery()
	{
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(SemesterPlansQueryRepository repository) : IQueryHandler<GetAllQuery, IReadOnlyCollection<SemesterPlan>>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<SemesterPlan>>> Handle(GetAllQuery query)
		{
			IReadOnlyCollection<SemesterPlan> plans = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<SemesterPlan>>(plans);
		}
	}
}
