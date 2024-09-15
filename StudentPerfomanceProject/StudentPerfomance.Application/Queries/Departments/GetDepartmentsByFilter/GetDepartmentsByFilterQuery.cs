using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilter;

internal sealed class GetDepartmentsByFilterQuery(IRepositoryExpression<TeachersDepartment> expression) : IQuery
{
	public IRepositoryExpression<TeachersDepartment> Expression { get; init; } = expression;
}
