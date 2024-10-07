using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Queries.All;

internal sealed class GetAllDirectionsQuery : IQuery
{
	private readonly EducationDirectionsQueryRepository _repository;
	public IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>> Handler;
	public GetAllDirectionsQuery()
	{
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(EducationDirectionsQueryRepository repository) : IQueryHandler<GetAllDirectionsQuery, IReadOnlyCollection<EducationDirection>>
	{
		private readonly EducationDirectionsQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<EducationDirection>>> Handle(GetAllDirectionsQuery query)
		{
			IReadOnlyCollection<EducationDirection> directions = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<EducationDirection>>(directions);
		}
	}
}
