using System.Linq.Expressions;

using SPerfomance.Application.Shared.Module.Extensions;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Users;

namespace SPerfomance.Application.Shared.Users.Module.Repositories.Expressions;

public class UsersFilter(
	string? name,
	string? surname,
	string? thirdname,
	string? email
) : IRepositoryExpression<User>
{
	private readonly string? _name = name.CreateValueOrEmpty();
	private readonly string? _surname = surname.CreateValueOrEmpty();
	private readonly string? _thirdName = thirdname.CreateValueOrEmpty();
	private readonly string? _email = email.CreateValueOrEmpty();

	public Expression<Func<User, bool>> Build() =>
		(User entity) =>
			(!string.IsNullOrWhiteSpace(_name) && entity.Name.Name.Contains(_name)) ||
			(!string.IsNullOrWhiteSpace(_surname) && entity.Name.Surname.Contains(_surname)) ||
			(!string.IsNullOrWhiteSpace(_thirdName) && !string.IsNullOrWhiteSpace(entity.Name.Thirdname) && entity.Name.Thirdname.Contains(_thirdName)) ||
			(!string.IsNullOrWhiteSpace(_email) && entity.Email.Email.Contains(_email));
}
