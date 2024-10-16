using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.Teachers.Module.Repository.Expressions;

internal sealed class Filter(TeacherRepositoryObject teacher) : IRepositoryExpression<Teacher>
{
	private readonly TeacherRepositoryObject _teacher = teacher;

	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
		(
			(!string.IsNullOrWhiteSpace(_teacher.Name) && entity.Name.Name.Contains(_teacher.Name)) ||
			(!string.IsNullOrWhiteSpace(_teacher.Surname) && entity.Name.Surname.Contains(_teacher.Surname)) ||
			(!string.IsNullOrWhiteSpace(_teacher.Thirdname) && entity.Name.Thirdname.Contains(_teacher.Thirdname)) ||
			(!string.IsNullOrWhiteSpace(_teacher.JobTitle) && entity.JobTitle.Value.Contains(_teacher.JobTitle)) ||
			(!string.IsNullOrWhiteSpace(_teacher.WorkingCondition) && entity.Condition.Value.Contains(_teacher.WorkingCondition))
		) &&
		entity.Department.FullName == _teacher.Department.Name && entity.Department.ShortName == _teacher.Department.ShortName;
}
