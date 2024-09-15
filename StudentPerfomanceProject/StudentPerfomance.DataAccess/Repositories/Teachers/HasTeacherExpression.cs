using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public class HasTeacherExpression(TeacherRepositoryParameter teacher, TeachersDepartmentRepositoryParameter department) : IRepositoryExpression<Teacher>
{
	private readonly TeacherRepositoryParameter _teacher = teacher;
	private readonly TeachersDepartmentRepositoryParameter _department = department;
	public Expression<Func<Teacher, bool>> Build() =>
		(Teacher entity) =>
			entity.Name.Name == _teacher.Name &&
			entity.Name.Surname == _teacher.Surname &&
			entity.Name.Thirdname == _teacher.Thirdname &&
			entity.Department.Name == _department.Name;
}
