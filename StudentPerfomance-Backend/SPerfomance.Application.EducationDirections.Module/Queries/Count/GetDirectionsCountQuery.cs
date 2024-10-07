using SPerfomance.Application.EducationDirections.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.EducationDirections.Module.Queries.Count;

internal sealed class GetDirectionsCountQuery : IQuery
{
	private readonly EducationDirectionsQueryRepository _repository;
	public readonly IQueryHandler<GetDirectionsCountQuery, int> Handler;
	public GetDirectionsCountQuery()
	{
		_repository = new EducationDirectionsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(EducationDirectionsQueryRepository repository) : IQueryHandler<GetDirectionsCountQuery, int>
	{
		private readonly EducationDirectionsQueryRepository _repository = repository;

		public async Task<OperationResult<int>> Handle(GetDirectionsCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
