using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters.Expressions;

internal sealed class SemestersFilter
(
	SemestersRepositoryObject semester
) : IRepositoryExpression<Semester>
{
	private readonly SemestersRepositoryObject _semester = semester;
	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			_semester.Number == entity.Number.Value ||
			_semester.PlanYear == entity.Plan.Year.Year;
}
