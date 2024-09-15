using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilter;

internal sealed class GetDepartmentsByFilterQueryHandler
(
	IRepository<TeachersDepartment> repository
)
: IQueryHandler<GetDepartmentsByFilterQuery, OperationResult<IReadOnlyCollection<TeachersDepartment>>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetDepartmentsByFilterQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFiltered(query.Expression);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		});
	}
}
