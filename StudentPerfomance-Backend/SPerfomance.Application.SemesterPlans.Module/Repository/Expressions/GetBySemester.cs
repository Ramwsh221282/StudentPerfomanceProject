using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;

internal sealed class GetBySemester(SemestersRepositoryObject semester) : IRepositoryExpression<SemesterPlan>
{
	private readonly SemestersRepositoryObject _semester = semester;

	public Expression<Func<SemesterPlan, bool>> Build() =>
		(SemesterPlan entity) =>
			entity.Semester.Number.Value == _semester.Number &&
			entity.Semester.Plan.Year.Year == _semester.Plan.Year &&
			entity.Semester.Plan.Direction.Name.Name == _semester.Plan.Direction.Name &&
			entity.Semester.Plan.Direction.Code.Code == _semester.Plan.Direction.Code &&
			entity.Semester.Plan.Direction.Type.Type == _semester.Plan.Direction.Type;

}
