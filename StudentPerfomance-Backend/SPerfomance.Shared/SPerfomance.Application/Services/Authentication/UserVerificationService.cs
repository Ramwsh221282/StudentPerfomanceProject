using SPerfomance.Application.Services.Authentication.Models;
using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.ValueObjects;

namespace SPerfomance.Application.Services.Authentication;

public class UserVerificationService(IUsersRepository repository)
{
    private readonly IUsersRepository _repository = repository;

    public async Task<bool> IsVerified(Token token, UserRole role)
    {
        if (token.IsExpired)
            return false;

        User? user = await _repository.GetById(token.UserId);
        if (user == null)
            return false;
        user.UpdateLoginDate();
        await _repository.UpdateLoginDate(user);
        return user.Role == role;
    }
}
