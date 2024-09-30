using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Disciplines;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans.Expressions;

internal sealed class HasSemesterPlan
(
	DisciplineRepositoryObject discipline,
	SemestersRepositoryObject semester
) : IRepositoryExpression<SemesterPlan>
{
	private readonly DisciplineRepositoryObject _discipline = discipline;
	private readonly SemestersRepositoryObject _semester = semester;

	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			entity.LinkedSemester.Number.Value == _semester.Number &&
			entity.LinkedDiscipline.Name == _discipline.Name;
}
