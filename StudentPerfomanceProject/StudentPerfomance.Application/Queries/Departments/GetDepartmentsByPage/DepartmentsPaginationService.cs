using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByPage;

public sealed class DepartmentsPaginationService
(
	int page,
	int pageSize,
	IRepository<TeachersDepartment> repository
)
: IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly GetDepartmentsByPageQuery _query = new GetDepartmentsByPageQuery(page, pageSize);
	private readonly GetDepartmentsByPageQueryHandler _handler = new GetDepartmentsByPageQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation() => await _handler.Handle(_query);
}
