using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByPage;

internal sealed class GetTeachersByPageQuery
(
	int page,
	int pageSize,
	IRepositoryExpression<Teacher> expression
)
: IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
	public IRepositoryExpression<Teacher> Expression { get; init; } = expression;
}
