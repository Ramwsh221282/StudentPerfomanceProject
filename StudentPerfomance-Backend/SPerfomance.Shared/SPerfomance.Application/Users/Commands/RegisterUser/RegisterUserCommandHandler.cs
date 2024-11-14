using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    string generatedPassword,
    IUsersRepository repository,
    IPasswordHasher hasher
) : ICommandHandler<RegisterUserCommand, User>
{
    private readonly string _generatedPassword = generatedPassword;

    private readonly IUsersRepository _repository = repository;

    private readonly IPasswordHasher _hasher = hasher;

    public async Task<Result<User>> Handle(RegisterUserCommand command)
    {
        if (await _repository.HasWithEmail(command.Email))
            return Result<User>.Failure(UserErrors.EmailDublicate(command.Email));

        string hashedPassword = _hasher.Hash(_generatedPassword);

        Result<User> user = User.Create(
            command.Name,
            command.Surname,
            command.Patronymic,
            command.Email,
            command.Role,
            hashedPassword
        );

        if (user.IsFailure)
            return user;

        await _repository.Insert(user.Value);
        return user;
    }
}
