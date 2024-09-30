using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;

internal sealed class TeachersByDepartment(DepartmentRepositoryObject department) : IRepositoryExpression<Teacher>
{
	private readonly DepartmentRepositoryObject _department = department;

	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) => entity.Department.FullName == _department.Name;
}
