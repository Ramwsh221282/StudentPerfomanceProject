using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByPage;

internal sealed class GetTeachersByPageQueryHandler
(
	IRepository<Teacher> repository
)
: IQueryHandler<GetTeachersByPageQuery, OperationResult<IReadOnlyCollection<Teacher>>>
{
	private readonly IRepository<Teacher> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Teacher>>> Handle(GetTeachersByPageQuery query)
	{
		IReadOnlyCollection<Teacher> teachers = await _repository.GetFilteredAndPaged(query.Expression, query.Page, query.PageSize);
		return new OperationResult<IReadOnlyCollection<Teacher>>(teachers);
	}
}
