using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.All;

internal sealed class GetAllTeacherDepartmentsQuery : IQuery
{
	private readonly TeacherDepartmentsQueryRepository _repository;
	public readonly IQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>> Handler;

	public GetAllTeacherDepartmentsQuery()
	{
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryHandler(_repository);
	}

	internal sealed class QueryHandler(TeacherDepartmentsQueryRepository repository) : IQueryHandler<GetAllTeacherDepartmentsQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetAllTeacherDepartmentsQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetAll();
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
