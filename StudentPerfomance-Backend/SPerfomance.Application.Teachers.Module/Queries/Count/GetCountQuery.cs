using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Queries.Count;

internal sealed class GetCountQuery : IQuery
{
	private readonly TeacherQueryRepository _repository;

	public IQueryHandler<GetCountQuery, int> Handler;

	public GetCountQuery(string token)
	{
		_repository = new TeacherQueryRepository();
		Handler = new QueryVerificaitonHandler<GetCountQuery, int>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetCountQuery, int>
	{
		private readonly TeacherQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetCountQuery, int> handler,
			TeacherQueryRepository repository)
		: base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<int>> Handle(GetCountQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
