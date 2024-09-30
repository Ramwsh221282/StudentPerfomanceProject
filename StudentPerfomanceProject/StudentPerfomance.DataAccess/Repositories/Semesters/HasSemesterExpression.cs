using System.Linq.Expressions;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Semesters;

public sealed class HasSemesterExpression
(
	SemestersRepositoryParameter semester,
	EducationPlanRepositoryParameter plan
)
: IRepositoryExpression<Semester>
{
	private readonly SemestersRepositoryParameter _semester = semester;
	private readonly EducationPlanRepositoryParameter _plan = plan;

	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			entity.Number.Value == _semester.Number &&
			entity.Plan.Year.Year == _plan.Year;
}
