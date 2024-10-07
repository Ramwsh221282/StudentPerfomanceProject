using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.StudentGroups.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly StudentGroupQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<StudentGroup>> Handler;
	public GetAllQuery()
	{
		_repository = new StudentGroupQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentGroupQueryRepository repository) : IQueryHandler<GetAllQuery, IReadOnlyCollection<StudentGroup>>
	{
		private readonly StudentGroupQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<StudentGroup>>> Handle(GetAllQuery query)
		{
			IReadOnlyCollection<StudentGroup> groups = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<StudentGroup>>(groups);
		}
	}
}
