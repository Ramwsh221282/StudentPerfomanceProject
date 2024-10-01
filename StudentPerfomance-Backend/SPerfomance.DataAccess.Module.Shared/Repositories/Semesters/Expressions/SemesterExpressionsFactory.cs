using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters.Expressions;

public static class SemesterExpressionsFactory
{
	public static IRepositoryExpression<Semester> CreateHasSemester(SemestersRepositoryObject semester) =>
			new HasSemester(semester);
	public static IRepositoryExpression<Semester> CreateFilter(SemestersRepositoryObject semester) =>
		new SemestersFilter(semester);
}
