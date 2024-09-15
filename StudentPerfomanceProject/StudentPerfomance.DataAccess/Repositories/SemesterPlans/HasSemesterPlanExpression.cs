using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.SemesterPlans;

public sealed class HasSemesterPlanExpression
(
	SemestersRepositoryParameter semester,
	DisciplineRepositoryParameter discipline
)
: IRepositoryExpression<SemesterPlan>
{
	private readonly SemestersRepositoryParameter _semester = semester;
	private readonly DisciplineRepositoryParameter _discipline = discipline;

	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			entity.LinkedSemester.Number.Value == _semester.Number &&
			entity.LinkedDiscipline.Name == _discipline.Name;
}
