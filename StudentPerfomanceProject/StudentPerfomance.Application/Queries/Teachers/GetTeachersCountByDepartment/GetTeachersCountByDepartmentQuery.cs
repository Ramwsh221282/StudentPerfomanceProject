using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersCountByDepartment;

internal sealed class GetTeachersCountByDepartmentQuery(IRepositoryExpression<Teacher> expression) : IQuery
{
	public IRepositoryExpression<Teacher> Expression { get; init; } = expression;
}
