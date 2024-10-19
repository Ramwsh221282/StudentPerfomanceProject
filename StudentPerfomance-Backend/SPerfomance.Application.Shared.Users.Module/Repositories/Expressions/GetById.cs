using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;

public sealed class GetById(string id) : IRepositoryExpression<User>
{
	private readonly string _id = id;

	public Expression<Func<User, bool>> Build() =>
		(User entity) => entity.Id.ToString() == _id;
}
