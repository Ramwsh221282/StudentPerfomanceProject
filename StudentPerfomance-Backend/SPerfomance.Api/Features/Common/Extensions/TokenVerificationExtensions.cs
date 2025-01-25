using SPerfomance.Domain.Models.Users.ValueObjects;

namespace SPerfomance.Api.Features.Common.Extensions;

public static class TokenVerificationExtensions
{
    public static async Task<bool> IsVerified(
        this Token token,
        IUsersRepository usersRepository,
        CancellationToken ct = default
    )
    {
        Console.WriteLine();
        var service = new UserVerificationService(usersRepository);
        var isAdmin = await service.IsVerified(token, UserRole.Administrator, ct);
        var isTeacher = await service.IsVerified(token, UserRole.Teacher, ct);
        return isAdmin || isTeacher;
    }

    public static async Task<bool> IsVerifiedAdmin(
        this Token token,
        IUsersRepository usersRepository,
        CancellationToken ct = default
    )
    {
        var service = new UserVerificationService(usersRepository);
        var isAdmin = await service.IsVerified(token, UserRole.Administrator, ct);
        return isAdmin;
    }

    public static async Task<bool> IsVerifiedTeacher(
        this Token token,
        IUsersRepository usersRepository,
        CancellationToken ct = default
    )
    {
        var service = new UserVerificationService(usersRepository);
        var isTeacher = await service.IsVerified(token, UserRole.Teacher, ct);
        return isTeacher;
    }
}
