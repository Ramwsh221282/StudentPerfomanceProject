using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;

internal sealed class GetSemester(SemesterPlanRepositoryObject plan) : IRepositoryExpression<Semester>
{
	private readonly SemesterPlanRepositoryObject _plan = plan;

	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			entity.Number.Value == _plan.Semester.Number &&
			entity.Plan.Year.Year == _plan.Semester.Plan.Year &&
			entity.Plan.Direction.Name.Name == _plan.Semester.Plan.Direction.Name &&
			entity.Plan.Direction.Type.Type == _plan.Semester.Plan.Direction.Type &&
			entity.Plan.Direction.Code.Code == _plan.Semester.Plan.Direction.Code;
}
