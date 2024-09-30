using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;

internal sealed class TeachersFilter(TeacherRepositoryObject teacher) : IRepositoryExpression<Teacher>
{
	private readonly TeacherRepositoryObject _teacher = teacher;

	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
			entity.Name.Name.Contains(_teacher.Name) ||
			entity.Name.Surname.Contains(_teacher.Surname) ||
			entity.Name.Thirdname.Contains(_teacher.Thirdname);
}
