using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.Disciplines;
using StudentPerfomance.DataAccess.Repositories.Semesters;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.SemesterPlans;

public sealed class SemesterPlansRepositoryFilterExpression
(
	StudentGroupsRepositoryParameter group,
	SemestersRepositoryParameter semester,
	DisciplineRepositoryParameter discipline
)
: IRepositoryExpression<SemesterPlan>
{
	private readonly StudentGroupsRepositoryParameter _group = group;
	private readonly SemestersRepositoryParameter _semester = semester;
	private readonly DisciplineRepositoryParameter _discipline = discipline;
	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			_semester.Number == entity.LinkedSemester.Number.Value &&
			!string.IsNullOrWhiteSpace(_discipline.Name) && entity.LinkedDiscipline.Name.Contains(_discipline.Name);
}
