using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Semester.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly SemesterQueryRepository _repository;
	public readonly IQueryHandler<GetCountQuery, int> Handler;
	public GetCountQuery()
	{
		_repository = new SemesterQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(SemesterQueryRepository repository) : IQueryHandler<GetCountQuery, int>
	{
		private readonly SemesterQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
