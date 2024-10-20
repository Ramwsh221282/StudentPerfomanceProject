using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.StudentGroups.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly StudentGroupQueryRepository _repository;

	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<StudentGroup>> Handler;

	public GetPagedQuery(int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryVerificaitonHandler<GetPagedQuery, IReadOnlyCollection<StudentGroup>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetPagedQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetPagedQuery, IReadOnlyCollection<StudentGroup>> handler,
			StudentGroupQueryRepository repository
			) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetPagedQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<StudentGroup> groups = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
