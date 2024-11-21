namespace SPerfomance.Api.Features.Common.Extensions;

public static class TokenVerificationExtensions
{
    public static async Task<bool> IsVerified(
        this TokenContract contract,
        IUsersRepository usersRepository
    )
    {
        UserVerificationService service = new UserVerificationService(usersRepository);
        Token token = contract;
        bool isAdmin = await service.IsVerified(token, UserRole.Administrator);
        bool isTeacher = await service.IsVerified(token, UserRole.Teacher);
        return isAdmin || isTeacher;
    }

    public static async Task<bool> IsVerifiedAdmin(
        this TokenContract contract,
        IUsersRepository usersRepository
    )
    {
        UserVerificationService service = new UserVerificationService(usersRepository);
        Token token = contract;
        bool isAdmin = await service.IsVerified(token, UserRole.Administrator);
        return isAdmin;
    }

    public static async Task<bool> IsVerifiedTeacher(
        this TokenContract contract,
        IUsersRepository usersRepository
    )
    {
        UserVerificationService service = new UserVerificationService(usersRepository);
        Token token = contract;
        bool isTeacher = await service.IsVerified(token, UserRole.Teacher);
        return isTeacher;
    }
}
