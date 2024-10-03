using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Queries;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Common.Models.OperationResults;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.All;

public sealed class GetAllEducationDirectionsQuery(IRepository<EducationDirection> repository) : IQuery
{
	public readonly IQueryHandler<GetAllEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler = new QueryHandler(repository);
	internal sealed class QueryHandler(IRepository<EducationDirection> repository) : IQueryHandler<GetAllEducationDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly IRepository<EducationDirection> _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetAllEducationDirectionsQuery query)
		{
			IReadOnlyCollection<EducationDirection> plans = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(plans);
		}
	}
}
