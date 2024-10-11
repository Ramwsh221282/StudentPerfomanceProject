using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.SemesterPlans.Module.Repository.Expressions;

internal sealed class GetSemester(SemestersRepositoryObject semester) : IRepositoryExpression<Semester>
{
	private readonly SemestersRepositoryObject _semester = semester;

	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			entity.Number.Value == _semester.Number &&
			entity.Plan.Year.Year == _semester.Plan.Year &&
			entity.Plan.Direction.Name.Name == _semester.Plan.Direction.Name &&
			entity.Plan.Direction.Type.Type == _semester.Plan.Direction.Type &&
			entity.Plan.Direction.Code.Code == _semester.Plan.Direction.Code;
}
