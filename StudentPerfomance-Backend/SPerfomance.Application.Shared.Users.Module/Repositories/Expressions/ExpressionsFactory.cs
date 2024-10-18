using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;

internal static class ExpressionsFactory
{
	public static IRepositoryExpression<User> GetByEmail(UserRepositoryObject user) => new GetByEmail(user);

	public static IRepositoryExpression<User> GetUser(UserRepositoryObject user) => new GetUser(user);
}
