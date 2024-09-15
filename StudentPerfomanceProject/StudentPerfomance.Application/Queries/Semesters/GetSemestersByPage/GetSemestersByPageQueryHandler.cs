using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Semesters.GetSemestersByPage;

internal sealed class GetSemestersByPageQueryHandler
(
	IRepository<Semester> repository
)
: IQueryHandler<GetSemestersByPageQuery, OperationResult<IReadOnlyCollection<Semester>>>
{
	private readonly IRepository<Semester> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Semester>>> Handle(GetSemestersByPageQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<Semester> semesters = await _repository.GetPaged(query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<Semester>>(semesters);
		});
	}
}
