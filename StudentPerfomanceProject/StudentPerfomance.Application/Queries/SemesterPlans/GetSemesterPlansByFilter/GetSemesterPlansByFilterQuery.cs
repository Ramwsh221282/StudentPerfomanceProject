using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansByFilter;

internal sealed class GetSemesterPlansByFilterQuery(int page, int pageSize, IRepositoryExpression<SemesterPlan> expression) : IQuery
{
	public int Page { get; init; } = page;
	public int PageSize { get; init; } = pageSize;
	public IRepositoryExpression<SemesterPlan> Expression { get; init; } = expression;
}
