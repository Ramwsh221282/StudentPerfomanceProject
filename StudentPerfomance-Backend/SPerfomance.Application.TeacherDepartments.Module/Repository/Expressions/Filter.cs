using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.TeachersDepartments;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository.Expressions;

internal sealed class Filter(DepartmentRepositoryObject department) : IRepositoryExpression<TeachersDepartment>
{
	private readonly DepartmentRepositoryObject _department = department;
	public Expression<Func<TeachersDepartment, bool>> Build() =>
		(TeachersDepartment entity) =>
			(!string.IsNullOrWhiteSpace(_department.Name) && entity.FullName.Contains(_department.Name)) ||
			(!string.IsNullOrWhiteSpace(_department.ShortName) && entity.ShortName.Contains(_department.ShortName));
}
