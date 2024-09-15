using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public static class TeacherExpressionFactory
{
	public static IRepositoryExpression<Teacher> CreateHasTeacher(TeacherRepositoryParameter teacher, TeachersDepartmentRepositoryParameter department) =>
		new HasTeacherExpression(teacher, department);
	public static IRepositoryExpression<Teacher> CreateByDepartment(TeachersDepartmentRepositoryParameter parameter) =>
		new TeacherByDepartmentExpression(parameter);
	public static IRepositoryExpression<Teacher> CreateFilter(TeacherRepositoryParameter teacher, TeachersDepartmentRepositoryParameter department) =>
		new TeachersFilterExpression(teacher, department);
	public static IRepositoryExpression<Teacher> CreateByName(TeacherRepositoryParameter parameter) =>
		new TeacherByNameExpression(parameter);
}
