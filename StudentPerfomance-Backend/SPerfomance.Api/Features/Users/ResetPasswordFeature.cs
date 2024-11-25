using Microsoft.AspNetCore.Mvc;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Users.Queries.GetUserByEmail;

namespace SPerfomance.Api.Features.Users;

public static class ResetPasswordFeature
{
    public record Request(GetUserByEmailQuery Query);

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IMailingService mailingService
    ) { }
}
