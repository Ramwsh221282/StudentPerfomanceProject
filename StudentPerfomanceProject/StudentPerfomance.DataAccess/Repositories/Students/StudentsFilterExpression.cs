using System.Linq.Expressions;

using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Students;

public sealed class StudentFilterExpression
(
	StudentsRepositoryParameter student,
	StudentGroupsRepositoryParameter group
)
: IRepositoryExpression<Student>
{
	private readonly StudentsRepositoryParameter _student = student;
	private readonly StudentGroupsRepositoryParameter _group = group;

	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			!string.IsNullOrWhiteSpace(_group.Name) && entity.Group.Name.Name == _group.Name &&
			(
				(!string.IsNullOrWhiteSpace(_student.Name) && entity.Name.Name.Contains(_student.Name)) ||
				(!string.IsNullOrWhiteSpace(_student.Surname) && entity.Name.Surname.Contains(_student.Surname)) ||
				(!string.IsNullOrWhiteSpace(_student.Thirdname) && entity.Name.Thirdname.Contains(_student.Thirdname)) ||
				(!string.IsNullOrWhiteSpace(_student.State) && entity.State.State == _student.State) ||
				entity.Recordbook.Recordbook == _student.Recordbook
			);
}
