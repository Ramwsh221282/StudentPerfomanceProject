using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByDepartment;

internal sealed class GetTeachersByDepartmentQuery(IRepositoryExpression<Teacher> expression) : IQuery
{
	public IRepositoryExpression<Teacher> Expression { get; init; } = expression;
}
