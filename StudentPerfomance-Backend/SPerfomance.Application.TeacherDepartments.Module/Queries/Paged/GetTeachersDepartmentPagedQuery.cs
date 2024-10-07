using SPerfomance.Application.Shared.Module.CQRS.Queries;
using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.TeacherDepartments.Module.Repository;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Queries.Paged;

internal sealed class GetTeachersDepartmentPagedQuery : IQuery
{
	private readonly int _page;
	private readonly int _pageSize;
	private readonly TeacherDepartmentsQueryRepository _repository;
	public readonly IQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>> Handler;
	public GetTeachersDepartmentPagedQuery(int page, int pageSize)
	{
		_page = page;
		_pageSize = pageSize;
		_repository = new TeacherDepartmentsQueryRepository();
		Handler = new QueryHandler(_repository);
	}
	internal sealed class QueryHandler(TeacherDepartmentsQueryRepository repository) : IQueryHandler<GetTeachersDepartmentPagedQuery, IReadOnlyCollection<TeachersDepartment>>
	{
		private readonly TeacherDepartmentsQueryRepository _repository = repository;
		public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetTeachersDepartmentPagedQuery query)
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetPaged(query._page, query._pageSize);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		}
	}
}
