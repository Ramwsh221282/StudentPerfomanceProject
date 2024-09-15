using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public sealed class TeacherByDepartmentExpression(TeachersDepartmentRepositoryParameter parameter) : IRepositoryExpression<Teacher>
{
	private readonly TeachersDepartmentRepositoryParameter _parameter = parameter;
	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) => entity.Department.Name == _parameter.Name;
}
