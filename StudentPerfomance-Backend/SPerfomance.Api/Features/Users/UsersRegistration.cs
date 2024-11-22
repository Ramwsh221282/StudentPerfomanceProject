using System.Text;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Users.Contracts;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Application.Services.Mailing;
using SPerfomance.Application.Services.Mailing.MailingMessages;
using SPerfomance.Application.Users.Commands.RegisterUser;
using SPerfomance.Application.Users.DTO;

namespace SPerfomance.Api.Features.Users;

public static class UsersRegistration
{
    public record Request(UserContract User, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{UserTags.Api}", Handler).WithTags(UserTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository repository,
        IPasswordGenerator generator,
        IPasswordHasher hasher,
        IMailingService mailing
    )
    {
        if (
            !await new UserVerificationService(repository).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(
                "Ваша сессия не удовлетворяет требованиям. Необходимо авторизоваться."
            );

        var generatedPass = generator.Generate();

        var user = await new RegisterUserCommandHandler(generatedPass, repository, hasher).Handle(
            new RegisterUserCommand(
                request.User.Name,
                request.User.Surname,
                request.User.Patronymic,
                request.User.Email,
                request.User.Role
            )
        );

        if (user.IsFailure)
            return Results.BadRequest(user.Error.Description);

        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine($"Почта: {user.Value.Email.Email}");
        messageBuilder.AppendLine($"Пароль: {generatedPass}");
        MailingMessage message = new UserRegistrationMessage(
            user.Value.Email.Email,
            messageBuilder.ToString()
        );
        var sending = mailing.SendMessage(message);
        return user.IsFailure
            ? Results.BadRequest(user.Error.Description)
            : Results.Ok(user.Value.MapFromDomain());
    }
}
