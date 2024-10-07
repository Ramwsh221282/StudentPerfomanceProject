using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.StudentGroups.Module.Repository;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly StudentGroupQueryRepository _repository;
	public readonly IQueryHandler<GetCountQuery, int> Handler;
	public GetCountQuery()
	{
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(StudentGroupQueryRepository repository) : IQueryHandler<GetCountQuery, int>
	{
		private readonly StudentGroupQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
