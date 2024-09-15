using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public sealed class TeachersFilterExpression(TeacherRepositoryParameter teacher, TeachersDepartmentRepositoryParameter department)
: IRepositoryExpression<Teacher>
{
	private readonly TeacherRepositoryParameter _teacher = teacher;
	private readonly TeachersDepartmentRepositoryParameter _department = department;
	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
			!string.IsNullOrWhiteSpace(_department.Name) && entity.Department.Name == _department.Name &&
			(
				(!string.IsNullOrWhiteSpace(_teacher.Name) && entity.Name.Name.Contains(_teacher.Name)) ||
				(!string.IsNullOrWhiteSpace(_teacher.Surname) && entity.Name.Surname.Contains(_teacher.Surname)) ||
				(!string.IsNullOrWhiteSpace(_teacher.Thirdname) && entity.Name.Thirdname.Contains(_teacher.Thirdname))
			);
}
