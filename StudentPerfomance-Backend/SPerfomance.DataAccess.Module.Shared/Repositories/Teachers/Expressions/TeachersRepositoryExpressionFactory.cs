using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;

public static class TeachersRepositoryExpressionFactory
{
	public static IRepositoryExpression<Teacher> CreateHasTeacher(TeacherRepositoryObject teacher, DepartmentRepositoryObject department) =>
			new HasTeacher(teacher, department);
	public static IRepositoryExpression<Teacher> CreateByDepartment(DepartmentRepositoryObject parameter) =>
		new TeachersByDepartment(parameter);
	public static IRepositoryExpression<Teacher> CreateFilter(TeacherRepositoryObject teacher) =>
		new TeachersFilter(teacher);
}
