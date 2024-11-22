using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUsersRepository repository)
    : IQueryHandler<GetUserByEmailQuery, User>
{
    public async Task<Result<User>> Handle(
        GetUserByEmailQuery command,
        CancellationToken ct = default
    )
    {
        var user = await repository.GetByEmail(command.Email, ct);
        return user == null
            ? Result<User>.Failure(UserErrors.NotFound())
            : Result<User>.Success(user);
    }
}
