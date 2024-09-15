using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilterAndPage;

public sealed class DepartmentsFilterWithPaginationService
(
	int page,
	int pageSize,
	IRepositoryExpression<TeachersDepartment> expression,
	IRepository<TeachersDepartment> repository
)
: IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly GetDepartmentsByFilterAndPageQuery _query = new GetDepartmentsByFilterAndPageQuery(page, pageSize, expression);
	private readonly GetDepartmentsByFilterAndPageQueryHandler _handler = new GetDepartmentsByFilterAndPageQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation() => await _handler.Handle(_query);
}
