using SPerfomance.Application.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Queries.GetUserByEmail;

public class GetUserByEmailQueryHandler(IUsersRepository repository)
    : IQueryHandler<GetUserByEmailQuery, User>
{
    private readonly IUsersRepository _repository = repository;

    public async Task<Result<User>> Handle(GetUserByEmailQuery command)
    {
        User? user = await _repository.GetByEmail(command.Email);
        if (user == null)
            return Result<User>.Failure(UserErrors.NotFound());

        return Result<User>.Success(user);
    }
}
