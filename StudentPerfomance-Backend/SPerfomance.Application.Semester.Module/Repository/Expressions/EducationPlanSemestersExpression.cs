using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Repository.Expressions;

internal sealed class EducationPlanSemestersExpression(EducationPlansRepositoryObject plan)
: IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester>
{
	private readonly EducationPlansRepositoryObject _plan = plan;

	public Expression<Func<Domain.Module.Shared.Entities.Semesters.Semester, bool>> Build() =>
		(Domain.Module.Shared.Entities.Semesters.Semester entity) =>
			entity.Plan.Year.Year == _plan.Year &&
			entity.Plan.Direction.Name.Name == _plan.Direction.Name &&
			entity.Plan.Direction.Code.Code == _plan.Direction.Code &&
			entity.Plan.Direction.Type.Type == _plan.Direction.Type;
}
