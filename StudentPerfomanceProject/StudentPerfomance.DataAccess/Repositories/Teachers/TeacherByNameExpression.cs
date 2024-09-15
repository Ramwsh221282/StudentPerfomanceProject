using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public sealed class TeacherByNameExpression(TeacherRepositoryParameter parameter) : IRepositoryExpression<Teacher>
{
	private readonly TeacherRepositoryParameter _parameter = parameter;

	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
			entity.Name.Name == _parameter.Name &&
			entity.Name.Surname == _parameter.Surname &&
			entity.Name.Thirdname == _parameter.Thirdname;
}
