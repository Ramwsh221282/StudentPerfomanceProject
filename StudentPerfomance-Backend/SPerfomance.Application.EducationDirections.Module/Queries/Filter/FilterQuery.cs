using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Filter;

internal sealed class FilterQuery : IQuery
{
	private readonly IRepositoryExpression<EducationDirection> _expression;
	private readonly int _page;
	private readonly int _pageSize;
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly IQueryHandler<FilterQuery, IReadOnlyCollection<EducationDirection>> Handler;
	public FilterQuery(EducationDirectionSchema direction, int page, int pageSize)
	{
		_expression = ExpressionsFactory.Filter(direction.ToRepositoryObject());
		_page = page;
		_pageSize = pageSize;
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(EducationDirectionsQueryRepository repository) : IQueryHandler<FilterQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(FilterQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetFilteredAndPaged(query._expression, query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
