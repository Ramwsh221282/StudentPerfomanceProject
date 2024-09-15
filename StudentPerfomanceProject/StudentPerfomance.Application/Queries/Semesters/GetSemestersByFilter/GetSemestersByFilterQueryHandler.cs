using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Semesters.GetSemestersByFilter;

internal sealed class GetSemestersByFilterQueryHandler
(
	IRepository<Semester> repository
)
: IQueryHandler<GetSemestersByFilterQuery, OperationResult<IReadOnlyCollection<Semester>>>
{
	private readonly IRepository<Semester> _repository = repository;
	public async Task<OperationResult<IReadOnlyCollection<Semester>>> Handle(GetSemestersByFilterQuery query)
	{
		return await this.ProcessAsync(async () =>
		{
			IReadOnlyCollection<Semester> semesters = await _repository.GetFilteredAndPaged(query.Expression, query.Page, query.PageSize);
			return new OperationResult<IReadOnlyCollection<Semester>>(semesters);
		});
	}
}
