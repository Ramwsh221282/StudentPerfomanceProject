using System.Linq.Expressions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;

public sealed class GetUser(UserRepositoryObject user) : IRepositoryExpression<User>
{
	private readonly UserRepositoryObject _user = user;

	public Expression<Func<User, bool>> Build() =>
		(User entity) =>
			entity.Email.Email == _user.Email &&
			entity.Name.Name == _user.Name &&
			entity.Name.Surname == _user.Surname &&
			entity.Name.Thirdname == _user.Thirdname;
}
