using System.Linq.Expressions;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.StudentGroups;

public sealed class StudentGroupsRepositoryFilterExpression(StudentGroupsRepositoryParameter parameter) : IRepositoryExpression<StudentGroup>
{
	private readonly StudentGroupsRepositoryParameter _parameter = parameter;

	public Expression<Func<StudentGroup, bool>> Build() =>
		(StudentGroup entity) =>
			!string.IsNullOrWhiteSpace(_parameter.Name) &&
			entity.Name.Name.Contains(_parameter.Name);
}
