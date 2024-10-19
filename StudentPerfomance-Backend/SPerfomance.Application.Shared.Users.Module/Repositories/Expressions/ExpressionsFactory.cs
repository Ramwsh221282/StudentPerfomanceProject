using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;

public static class ExpressionsFactory
{
	public static IRepositoryExpression<User> GetByEmail(UserRepositoryObject user) => new GetByEmail(user);

	public static IRepositoryExpression<User> GetUser(UserRepositoryObject user) => new GetUser(user);

	public static IRepositoryExpression<User> GetById(string id) => new GetById(id);

	public static IRepositoryExpression<User> Filter(
		string? name,
		string? surname,
		string? thirdname,
		string? email
		) => new UsersFilter(name, surname, thirdname, email);
}
