using SPerfomance.Application.SemesterPlans.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.SemesterPlans.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly SemesterPlansQueryRepository _repository;
	public readonly IQueryHandler<GetCountQuery, int> Handler;
	public GetCountQuery()
	{
		_repository = new SemesterPlansQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(SemesterPlansQueryRepository repository) : IQueryHandler<GetCountQuery, int>
	{
		private readonly SemesterPlansQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
