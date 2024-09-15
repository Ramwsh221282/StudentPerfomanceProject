using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Departments.GetDepartmentsByFilterAndPage;

internal sealed class GetDepartmentsByFilterAndPageQueryHandler
(
	IRepository<TeachersDepartment> repository
)
: IQueryHandler<GetDepartmentsByFilterAndPageQuery, OperationResult<IReadOnlyCollection<TeachersDepartment>>>
{
	private readonly IRepository<TeachersDepartment> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<TeachersDepartment>>> Handle(GetDepartmentsByFilterAndPageQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<TeachersDepartment> departments = await _repository.GetFilteredAndPaged(query.Expression, query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<TeachersDepartment>>(departments);
		});
	}
}
