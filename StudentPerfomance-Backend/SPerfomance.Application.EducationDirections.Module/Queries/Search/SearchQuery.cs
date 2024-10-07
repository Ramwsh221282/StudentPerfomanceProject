using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.EducationDirections.Module.Repository.Expressions;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Search;

internal sealed class SearchQuery : IQuery
{
	private readonly IRepositoryExpression<EducationDirection> _expression;
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly IQueryHandler<SearchQuery, IReadOnlyCollection<EducationDirection>> Handler;
	public SearchQuery(EducationDirectionSchema schema)
	{
		_expression = ExpressionsFactory.Filter(schema.ToRepositoryObject());
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(EducationDirectionsQueryRepository repository) : IQueryHandler<SearchQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(SearchQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
