using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.GetPagedFiltered;

public sealed class GetPagedFilteredEducationDirectionsQuery
(
	int page,
	int pageSize,
	IRepositoryExpression<EducationDirection> expression,
	IRepository<EducationDirection> repository
) : IQuery
{
	private readonly int _page = page;
	private readonly int _pageSize = pageSize;
	private readonly IRepositoryExpression<EducationDirection> _expression = expression;
	public readonly IQueryHandler<GetPagedFilteredEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationDirection> repository) : IQueryHandler<GetPagedFilteredEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly IRepository<EducationDirection> _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetPagedFilteredEducationDirectionsQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
