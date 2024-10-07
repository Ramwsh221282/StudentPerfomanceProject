using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Students.Module.Repository;

namespace SPerfomance.Application.Students.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly StudentQueryRepository _repository;
	public readonly IQueryHandler<GetCountQuery, int> Handler;
	public GetCountQuery()
	{
		_repository = new StudentQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(StudentQueryRepository repository) : IQueryHandler<GetCountQuery, int>
	{
		private readonly StudentQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
