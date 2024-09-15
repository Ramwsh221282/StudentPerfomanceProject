using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.TeachersDepartments;

public static class TeacherDepartmentsExpressionFactory
{
	public static IRepositoryExpression<TeachersDepartment> CreateHasDepartmentExpression(TeachersDepartmentRepositoryParameter parameter) =>
		new HasDepartmentExpression(parameter);
	public static IRepositoryExpression<TeachersDepartment> CreateFilterExpression(TeachersDepartmentRepositoryParameter parameter) =>
		new TeacherDepartmentsRepositoryFilterExpression(parameter);
}
