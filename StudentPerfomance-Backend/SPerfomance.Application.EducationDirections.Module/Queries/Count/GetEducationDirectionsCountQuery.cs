using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Count;

public sealed class GetEducationDirectionsCountQuery(IRepository<EducationDirection> repository) : IQuery
{
	public readonly IQueryHandler<GetEducationDirectionsCountQuery, int> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationDirection> repository) : IQueryHandler<GetEducationDirectionsCountQuery, int>
	{
		private readonly IRepository<EducationDirection> _repository = repository;
		public async Task<OperationResult<int>> Handle(GetEducationDirectionsCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
