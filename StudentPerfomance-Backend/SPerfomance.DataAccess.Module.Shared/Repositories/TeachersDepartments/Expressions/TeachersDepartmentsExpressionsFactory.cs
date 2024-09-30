using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments.Expressions;

public static class TeachersDepartmentsExpressionsFactory
{
	public static IRepositoryExpression<TeachersDepartment> HasDepartment(DepartmentRepositoryObject department) =>
		new HasDepartment(department);

	public static IRepositoryExpression<TeachersDepartment> Filter(DepartmentRepositoryObject department) =>
		new DepartmentFilter(department);
}
