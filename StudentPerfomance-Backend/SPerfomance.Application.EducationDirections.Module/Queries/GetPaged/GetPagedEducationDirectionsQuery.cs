using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Users.Module.Queries.Common;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.EducationDirections.Module.Queries.GetPaged;

internal sealed class GetPagedEducationDirectionsQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly IQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler;
	public GetPagedEducationDirectionsQuery(int page, int pageSize, string token)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryVerificaitonHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>(token, User.Admin);
		Handler = new QueryHandler(Handler, _repository);
	}

	internal sealed class QueryHandler : DecoratedQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository;

		public QueryHandler(
			IQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> handler,
			EducationDirectionsQueryRepository repository)
		 : base(handler)
		{
			_repository = repository;
		}

		public override async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetPagedEducationDirectionsQuery query)
		{
			OperationResult<IReadOnlyCollection<EducationDirection>> result = await base.Handle(query);
			if (result.IsFailed)
				return result;

			IReadOnlyCollection<EducationDirection> directions = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
