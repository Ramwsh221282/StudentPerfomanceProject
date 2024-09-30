using System.Linq.Expressions;

using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Students.Expressions;

internal sealed class StudentsByGroup(StudentGroupsRepositoryObject group) : IRepositoryExpression<Student>
{
	private readonly StudentGroupsRepositoryObject _group = group;
	public Expression<Func<Student, bool>> Build() =>
		(Student entity) =>
			entity.Group.Name.Name == _group.Name;
}
