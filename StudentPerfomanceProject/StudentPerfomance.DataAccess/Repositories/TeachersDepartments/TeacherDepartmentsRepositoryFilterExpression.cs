using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.TeachersDepartments;

public sealed class TeacherDepartmentsRepositoryFilterExpression(TeachersDepartmentRepositoryParameter parameter) : IRepositoryExpression<TeachersDepartment>
{
	private readonly TeachersDepartmentRepositoryParameter _parameter = parameter;
	public Expression<Func<TeachersDepartment, bool>> Build() =>
		(TeachersDepartment entity) =>
			!string.IsNullOrWhiteSpace(_parameter.Name) &&
			entity.Name.Contains(_parameter.Name);
}
