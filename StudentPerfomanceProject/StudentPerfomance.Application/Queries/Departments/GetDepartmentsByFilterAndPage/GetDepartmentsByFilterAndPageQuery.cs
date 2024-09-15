using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilterAndPage;

internal sealed class GetDepartmentsByFilterAndPageQuery
(
	int page,
	int pageSize,
	IRepositoryExpression<TeachersDepartment> expression
)
: IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
	public IRepositoryExpression<TeachersDepartment> Expression { get; init; } = expression;
}
