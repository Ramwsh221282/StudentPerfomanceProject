using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Teachers.Module.Repository;

namespace SPerfomance.Application.Teachers.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly TeacherQueryRepository _repository;
	public IQueryHandler<GetCountQuery, int> Handler;
	public GetCountQuery()
	{
		_repository = new TeacherQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherQueryRepository repository) : IQueryHandler<GetCountQuery, int>
	{
		private readonly TeacherQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
