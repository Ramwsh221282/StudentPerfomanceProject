using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Teachers.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly TeacherQueryRepository _repository;
	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<Teacher>> Handler;

	public GetPagedQuery(int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherQueryRepository();
		Handler = new QueryVerificaitonHandler<GetPagedQuery, IReadOnlyCollection<Teacher>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetPagedQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetPagedQuery, IReadOnlyCollection<Teacher>> handler,
			TeacherQueryRepository repository)
			 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetPagedQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Teacher> teachers = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}
