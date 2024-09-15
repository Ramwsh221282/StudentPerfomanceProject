using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Semesters.GetSemestersByFilter;

internal sealed class GetSemestersByFilterQuery
(
	int page,
	int pageSize,
	IRepositoryExpression<Semester> expression
)
: IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
	public IRepositoryExpression<Semester> Expression { get; init; } = expression;
}
