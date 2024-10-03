using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.GetFiltered;

public sealed class FilterEducationDirectionsQuery
(
	IRepositoryExpression<EducationDirection> expression,
	IRepository<EducationDirection> repository
) : IQuery
{
	private readonly IRepositoryExpression<EducationDirection> _expression = expression;
	public readonly IQueryHandler<FilterEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationDirection> repository) : IQueryHandler<FilterEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly IRepository<EducationDirection> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(FilterEducationDirectionsQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetFiltered(query._expression);
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
