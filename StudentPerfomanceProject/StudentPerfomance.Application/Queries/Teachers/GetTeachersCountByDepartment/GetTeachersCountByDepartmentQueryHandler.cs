using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersCountByDepartment;

internal sealed class GetTeachersCountByDepartmentQueryHandler
(
	IRepository<Teacher> repository
)
: IQueryHandler<GetTeachersCountByDepartmentQuery, OperationResult<int>>
{
	private readonly IRepository<Teacher> _repository = repository;

	public async Task<OperationResult<int>> Handle(GetTeachersCountByDepartmentQuery query)
	{
		int count = await _repository.CountWithExpression(query.Expression);
		return new OperationResult<int>(count);
	}
}
