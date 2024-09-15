using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersCountByDepartment;

public sealed class TeachersCountByDepartmentService
(
	IRepositoryExpression<Teacher> expression,
	IRepository<Teacher> repository
)
: IService<int>
{
	private readonly GetTeachersCountByDepartmentQuery _query = new GetTeachersCountByDepartmentQuery(expression);
	private readonly GetTeachersCountByDepartmentQueryHandler _handler = new GetTeachersCountByDepartmentQueryHandler(repository);
	public async Task<OperationResult<int>> DoOperation() => await _handler.Handle(_query);
}
