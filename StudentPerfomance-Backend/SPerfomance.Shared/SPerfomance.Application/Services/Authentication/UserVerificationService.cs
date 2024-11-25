using SPerfomance.Application.Services.Authentication.Models;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Models.Users.ValueObjects;

namespace SPerfomance.Application.Services.Authentication;

public class UserVerificationService(IUsersRepository repository)
{
    public async Task<bool> IsVerified(Token token, UserRole role, CancellationToken ct = default)
    {
        if (token.IsExpired)
            return false;

        var user = await repository.GetById(token.UserId, ct);
        if (user == null)
            return false;
        user.UpdateLoginDate();
        await repository.UpdateLoginDate(user, ct);
        return user.Role == role;
    }

    public async Task<bool> IsVerifiedAny(Token token, CancellationToken ct = default)
    {
        if (token.IsExpired)
            return false;

        var user = await repository.GetById(token.UserId, ct);
        if (user == null)
            return false;
        user.UpdateLoginDate();
        return true;
    }
}
