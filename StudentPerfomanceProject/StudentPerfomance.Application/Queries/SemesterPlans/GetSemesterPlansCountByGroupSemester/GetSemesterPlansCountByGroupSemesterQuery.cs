using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Queries.SemesterPlans.GetSemesterPlansCountByGroupSemester;

internal sealed class GetSemesterPlansCountByGroupSemesterQuery(IRepositoryExpression<SemesterPlan> expression) : IQuery
{
	public IRepositoryExpression<SemesterPlan> Expression { get; init; } = expression;
}
