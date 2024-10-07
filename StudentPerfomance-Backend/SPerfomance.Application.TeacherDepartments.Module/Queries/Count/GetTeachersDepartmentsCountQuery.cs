using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.TeacherDepartments.Module.Repository;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Count;

internal sealed class GetTeachersDepartmentsCountQuery : IQuery
{
	private readonly TeacherDepartmentsQueryRepository _repository;
	public readonly IQueryHandler<GetTeachersDepartmentsCountQuery, int> Handler;

	public GetTeachersDepartmentsCountQuery()
	{
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherDepartmentsQueryRepository repository) : IQueryHandler<GetTeachersDepartmentsCountQuery, int>
	{
		private readonly TeacherDepartmentsQueryRepository _repository = repository;
		public async Task<OperationResult<int>> Handle(GetTeachersDepartmentsCountQuery query)
		{
			int count = await _repository.Count();
			return new OperationResult<int>(count);
		}
	}
}
