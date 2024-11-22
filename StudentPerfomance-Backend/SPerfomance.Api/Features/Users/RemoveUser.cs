using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Users.Contracts;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;
using SPerfomance.Application.Users.Commands.RemoveUser;
using SPerfomance.Application.Users.DTO;
using SPerfomance.Application.Users.Queries.GetUserByEmail;

namespace SPerfomance.Api.Features.Users;

public static class RemoveUser
{
    public record Request(UserContract User, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{UserTags.Api}", Handler).WithTags(UserTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository repository,
        IMailingService service,
        CancellationToken ct
    )
    {
        if (
            !await new UserVerificationService(repository).IsVerified(
                request.Token,
                UserRole.Administrator,
                ct
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        var user = await new GetUserByEmailQueryHandler(repository).Handle(request.User, ct);
        user = await new RemoveUserCommandHandler(repository).Handle(
            new RemoveUserCommand(user.Value),
            ct
        );

        if (user.IsFailure)
            return Results.BadRequest(user.Error.Description);

        MailingMessage message = new UserRemoveMessage(user.Value.Email.Email);
        var sending = service.SendMessage(message);
        return Results.Ok(user.Value.MapFromDomain());
    }
}
