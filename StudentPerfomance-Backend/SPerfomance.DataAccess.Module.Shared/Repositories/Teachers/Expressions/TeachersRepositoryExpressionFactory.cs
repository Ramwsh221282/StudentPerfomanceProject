using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;

public static class TeachersRepositoryExpressionFactory
{
	public static IRepositoryExpression<Teacher> CreateHasTeacher(TeacherRepositoryObject teacher) =>
			new HasTeacher(teacher);
	public static IRepositoryExpression<Teacher> CreateFilter(TeacherRepositoryObject teacher) =>
		new TeachersFilter(teacher);
}
