using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.GetPaged;

internal sealed class GetPagedEducationDirectionsQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly IQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler;
	public GetPagedEducationDirectionsQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(EducationDirectionsQueryRepository repository) : IQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetPagedEducationDirectionsQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
