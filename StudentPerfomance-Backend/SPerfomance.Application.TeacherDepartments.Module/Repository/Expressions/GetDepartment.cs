using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;

internal sealed class GetDepartment(DepartmentRepositoryObject department) : IRepositoryExpression<TeachersDepartment>
{
	private readonly DepartmentRepositoryObject _department = department;

	public Expression<Func<TeachersDepartment, bool>> Build() =>
		(TeachersDepartment entity) =>
			entity.FullName == _department.Name;
}
