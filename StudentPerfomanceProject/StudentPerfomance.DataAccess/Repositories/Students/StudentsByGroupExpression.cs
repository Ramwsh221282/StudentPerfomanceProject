using System.Linq.Expressions;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Students;

public sealed class StudentsByGroupExpression(StudentGroupsRepositoryParameter parameter) : IRepositoryExpression<Student>
{
	private readonly StudentGroupsRepositoryParameter _parameter = parameter;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			entity.Group.Name.Name == _parameter.Name;
}
