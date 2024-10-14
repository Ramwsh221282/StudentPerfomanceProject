using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<TeachersDepartment> GetDepartment(DepartmentRepositoryObject department) =>
		new GetDepartment(department);

	public static IRepositoryExpression<TeachersDepartment> Filter(DepartmentRepositoryObject department) =>
		new Filter(department);

	public static IRepositoryExpression<Teacher> GetTeacher(TeacherRepositoryObject teacher) =>
		new GetTeacher(teacher);

	public static IRepositoryExpression<Teacher> GetDepartmentTeachers(DepartmentRepositoryObject department) =>
		new GetDepartmentTeachers(department);

	public static IRepositoryExpression<Teacher> FilterTeachers(TeacherRepositoryObject teacher) =>
		new TeacherFilter(teacher);
}
