using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters.Expressions;

internal sealed class HasSemester
(
	SemestersRepositoryObject semester,
	EducationPlansRepositoryObject plan
) : IRepositoryExpression<Semester>
{
	private readonly SemestersRepositoryObject _semester = semester;
	private readonly EducationPlansRepositoryObject _plan = plan;

	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			entity.Number.Value == _semester.Number &&
			entity.Plan.Year.Year == _plan.Year;
}
