using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Teachers.Expressions;

internal sealed class HasTeacher
(
	TeacherRepositoryObject teacher
) : IRepositoryExpression<Teacher>
{
	private readonly TeacherRepositoryObject _teacher = teacher;
	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
			entity.Name.Name == _teacher.Name &&
			entity.Name.Surname == _teacher.Surname &&
			entity.Name.Thirdname == _teacher.Thirdname &&
			entity.JobTitle.Value == _teacher.JobTitle &&
			entity.Condition.Value == _teacher.WorkingCondition;
}
