using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.SemesterPlans;

public sealed class SemesterPlansByGroupSemesterExpression
(
	SemestersRepositoryParameter semester,
	StudentGroupsRepositoryParameter group
)
: IRepositoryExpression<SemesterPlan>
{
	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			entity.LinkedSemester.Number.Value == semester.Number;
}
