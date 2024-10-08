using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;

internal sealed class GetPlan(SemesterPlanRepositoryObject plan) : IRepositoryExpression<SemesterPlan>
{
	private readonly SemesterPlanRepositoryObject _plan = plan;

	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			entity.Discipline.Name == _plan.DisciplineName &&
			entity.Semester.Number.Value == _plan.Semester.Number &&
			entity.Semester.Plan.Year.Year == _plan.Semester.Plan.Year &&
			entity.Semester.Plan.Direction.Name.Name == _plan.Semester.Plan.Direction.Name &&
			entity.Semester.Plan.Direction.Code.Code == _plan.Semester.Plan.Direction.Code &&
			entity.Semester.Plan.Direction.Type.Type == _plan.Semester.Plan.Direction.Type;
}
