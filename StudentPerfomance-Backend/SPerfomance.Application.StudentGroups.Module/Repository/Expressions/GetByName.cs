using System.Linq.Expressions;
using SPerfomance.DataAccess.Module.Shared.Repositories.StudentGroups;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Repository.Expressions;

internal sealed class GetByName(StudentGroupsRepositoryObject group) : IRepositoryExpression<StudentGroup>
{
	private readonly StudentGroupsRepositoryObject _group = group;

	public Expression<Func<StudentGroup, bool>> Build() =>
		(StudentGroup entity) => entity.Name.Name == _group.Name;
}
