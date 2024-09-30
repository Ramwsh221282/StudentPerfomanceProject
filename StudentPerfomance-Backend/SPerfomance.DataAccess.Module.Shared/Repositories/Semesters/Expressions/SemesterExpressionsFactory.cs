using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters.Expressions;

public static class SemesterExpressionsFactory
{
	public static IRepositoryExpression<Semester> CreateHasSemester(SemestersRepositoryObject semester, EducationPlansRepositoryObject plan) =>
			new HasSemester(semester, plan);
	public static IRepositoryExpression<Semester> CreateFilter(SemestersRepositoryObject semester) =>
		new SemestersFilter(semester);
}
