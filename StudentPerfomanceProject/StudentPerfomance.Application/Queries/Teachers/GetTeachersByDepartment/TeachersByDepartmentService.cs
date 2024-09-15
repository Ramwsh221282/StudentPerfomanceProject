using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByDepartment;

public sealed class TeachersByDepartmentService
(
	IRepositoryExpression<Teacher> expression,
	IRepository<Teacher> repository
)
: IService<IReadOnlyCollection<Teacher>>
{
	private readonly GetTeachersByDepartmentQuery _query = new GetTeachersByDepartmentQuery(expression);
	private readonly GetTeachersByDepartmentQueryHandler _handler = new GetTeachersByDepartmentQueryHandler(repository);
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> DoOperation() => await _handler.Handle(_query);
}
