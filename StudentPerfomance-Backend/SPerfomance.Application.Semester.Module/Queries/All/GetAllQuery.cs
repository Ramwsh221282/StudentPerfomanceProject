using SPerfomance.Application.Semester.Module.Repository;
using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;

namespace SPerfomance.Application.Semester.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly SemesterQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> Handler;
	public GetAllQuery()
	{
		_repository = new SemesterQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(SemesterQueryRepository repository) : IQueryHandler<GetAllQuery, IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>
	{
		private readonly SemesterQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>> Handle(GetAllQuery query)
		{
			IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester> semesters = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>>(semesters);
		}
	}
}
