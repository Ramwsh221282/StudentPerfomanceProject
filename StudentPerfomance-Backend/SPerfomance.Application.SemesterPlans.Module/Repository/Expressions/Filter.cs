using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;

internal sealed class Filter(SemesterPlanRepositoryObject plan) : IRepositoryExpression<SemesterPlan>
{
	private readonly SemesterPlanRepositoryObject _plan = plan;

	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			(!string.IsNullOrWhiteSpace(_plan.DisciplineName) && entity.Discipline.Name.Contains(_plan.DisciplineName)) ||
			(_plan.Semester.Number == entity.Semester.Number.Value) ||
			(_plan.Semester.Plan.Year == entity.Semester.Plan.Year.Year) ||
			(!string.IsNullOrWhiteSpace(_plan.Semester.Plan.Direction.Name) && entity.Semester.Plan.Direction.Name.Name.Contains(_plan.Semester.Plan.Direction.Name)) ||
			(!string.IsNullOrWhiteSpace(_plan.Semester.Plan.Direction.Code) && entity.Semester.Plan.Direction.Code.Code.Contains(_plan.Semester.Plan.Direction.Name));
}
