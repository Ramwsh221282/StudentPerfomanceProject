using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilter;

public sealed class DepartmentsFilterService
(
	IRepositoryExpression<TeachersDepartment> expression,
	IRepository<TeachersDepartment> repository
)
: IService<IReadOnlyCollection<TeachersDepartment>>
{
	private readonly GetDepartmentsByFilterQuery _query = new GetDepartmentsByFilterQuery(expression);
	private readonly GetDepartmentsByFilterQueryHandler _handler = new GetDepartmentsByFilterQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> DoOperation() => await _handler.Handle(_query);
}
