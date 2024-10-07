using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Students.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly StudentQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<Student>> Handler;
	public GetAllQuery()
	{
		_repository = new StudentQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(StudentQueryRepository repository) : IQueryHandler<GetAllQuery, IReadOnlyCollection<Student>>
	{
		private readonly StudentQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<Student>>> Handle(GetAllQuery query)
		{
			IReadOnlyCollection<Student> students = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<Student>>(students);
		}
	}
}
