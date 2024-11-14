using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQuery(string? email) : IQuery<User>
{
    public string Email { get; init; } = email.ValueOrEmpty();
}
