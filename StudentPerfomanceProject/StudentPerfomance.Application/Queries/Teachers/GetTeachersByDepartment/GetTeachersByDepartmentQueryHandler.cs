using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByDepartment;

internal sealed class GetTeachersByDepartmentQueryHandler
(
	IRepository<Teacher> repository
)
: IQueryHandler<GetTeachersByDepartmentQuery, OperationResult<IReadOnlyCollection<Teacher>>>
{
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetTeachersByDepartmentQuery query)
	{
		IReadOnlyCollection<Teacher> teachers = await _repository.GetFiltered(query.Expression);
		return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
	}
}
