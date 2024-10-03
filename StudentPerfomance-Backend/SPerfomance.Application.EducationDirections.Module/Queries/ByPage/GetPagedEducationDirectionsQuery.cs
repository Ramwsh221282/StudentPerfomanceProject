using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.ByPage;

public sealed class GetPagedEducationDirectionsQuery(int page, int pageSize, IRepository<EducationDirection> repository) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	public readonly IQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationDirection> repository) : IQueryHandler<GetPagedEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly IRepository<EducationDirection> _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetPagedEducationDirectionsQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
