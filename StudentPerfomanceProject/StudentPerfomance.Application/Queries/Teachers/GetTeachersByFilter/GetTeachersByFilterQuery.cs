using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.Teachers.GetTeachersByFilter;

internal sealed class GetTeachersByFilterQuery : IQuery
{
	public int Page { get; init; }
	public int PageSize { get; init; }
	public IRepositoryExpression<Teacher> Expression { get; init; }
	public GetTeachersByFilterQuery(int page, int pageSize, IRepositoryExpression<Teacher> expression)
	{
		Page = page;
		PageSize = pageSize;
		Expression = expression;
	}
}
