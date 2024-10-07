using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.Teachers;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.Teachers.Module.Repository.Expressions;

internal sealed class GetDepartment(TeacherRepositoryObject teacher) : IRepositoryExpression<TeachersDepartment>
{
	private readonly TeacherRepositoryObject _teacher = teacher;
	public Expression<Func<TeachersDepartment, bool>> Build() =>
		(TeachersDepartment entity) =>
			entity.FullName == _teacher.Department.Name;
}
