using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlans;

internal sealed class GetSemesterPlansQuery(int page, int pageSize, IRepositoryExpression<SemesterPlan> expression) : IQuery
{
	public IRepositoryExpression<SemesterPlan> Expression { get; init; } = expression;
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
}
