using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Students;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Students.Module.Queries.Paged;

internal sealed class GetPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly StudentQueryRepository _repository;

	public readonly IQueryHandler<GetPagedQuery, IReadOnlyCollection<Student>> Handler;

	public GetPagedQuery(int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new StudentQueryRepository();
		Handler = new QueryVerificaitonHandler<GetPagedQuery, IReadOnlyCollection<Student>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetPagedQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetPagedQuery, IReadOnlyCollection<Student>> handler,
			StudentQueryRepository repository) : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetPagedQuery query)
		{
			var result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<Student> students = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
