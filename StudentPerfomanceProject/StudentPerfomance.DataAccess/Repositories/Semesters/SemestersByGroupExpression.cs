using System.Linq.Expressions;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Semesters;

public sealed class SemestersByGroupExpression(StudentGroupsRepositoryParameter group) : IRepositoryExpression<Semester>
{
	private readonly StudentGroupsRepositoryParameter _group = group;

	public Expression<Func<Semester, bool>> Build() =>
		(Semester entity) =>
			!string.IsNullOrWhiteSpace(_group.Name) &&
			entity.Group.Name.Name == _group.Name;
}
