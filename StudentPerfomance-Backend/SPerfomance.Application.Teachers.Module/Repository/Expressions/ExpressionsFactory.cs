using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<Teacher> GetTeacher(TeacherRepositoryObject teacher) =>
		new GetTeacher(teacher);

	public static IRepositoryExpression<TeachersDepartment> GetDepartment(DepartmentRepositoryObject department) =>
		new GetDepartment(department);

	public static IRepositoryExpression<Teacher> Filter(TeacherRepositoryObject teacher) =>
		new Filter(teacher);
}
