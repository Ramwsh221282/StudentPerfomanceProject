using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;

internal sealed class GetDepartmentTeachers(DepartmentRepositoryObject department) : IRepositoryExpression<Teacher>
{
	private readonly DepartmentRepositoryObject _department = department;
	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
			entity.Department.FullName == _department.Name && entity.Department.ShortName == _department.ShortName;
}
