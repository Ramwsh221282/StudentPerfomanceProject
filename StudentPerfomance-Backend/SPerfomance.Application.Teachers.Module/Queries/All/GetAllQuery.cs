using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Teachers.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Queries.All;

internal sealed class GetAllQuery : IQuery
{
	private readonly TeacherQueryRepository _repository;
	public readonly IQueryHandler<GetAllQuery, IReadOnlyCollection<Teacher>> Handler;
	public GetAllQuery()
	{
		_repository = new TeacherQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherQueryRepository repository) : IQueryHandler<GetAllQuery, IReadOnlyCollection<Teacher>>
	{
		private readonly TeacherQueryRepository _repository = repository;

		public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetAllQuery query)
		{
			IReadOnlyCollection<Teacher> teachers = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
		}
	}
}
