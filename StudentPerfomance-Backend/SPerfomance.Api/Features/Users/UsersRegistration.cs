using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
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
            app.MapPost($"{UserTags.Api}", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("UsersRegistration")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Добавление пользователя в систему")
                        .AppendLine("Результат ОК (200): Добавленный пользователь.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<UserDto>>
    > Handler(
        Request request,
        IUsersRepository repository,
        IPasswordGenerator generator,
        IPasswordHasher hasher,
        IMailingService mailing,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на регистрацию пользователя в системе");
        var jwtToken = new Token(request.Token.Token);
        if (!await jwtToken.IsVerified(repository, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

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
        {
            logger.LogError(
                "Запрос пользователя {id} на регистрацию пользователя в системе отменен. Причина: {text}",
                jwtToken.UserId,
                user.Error.Description
            );
            return TypedResults.BadRequest(user.Error.Description);
        }

        var messageBuilder = new StringBuilder();
        messageBuilder.AppendLine($"Почта: {user.Value.Email.Email}");
        messageBuilder.AppendLine($"Пароль: {generatedPass}");
        MailingMessage message = new UserRegistrationMessage(
            user.Value.Email.Email,
            messageBuilder.ToString()
        );

        var sending = mailing.SendMessage(message);

        logger.LogInformation(
            "Пользователь {id} добавил пользователя {uid} {uname} {usurname} {uemail} {urole} в систему",
            jwtToken.UserId,
            user.Value.Id,
            user.Value.Name.Name,
            user.Value.Name.Surname,
            user.Value.Email.Email,
            user.Value.Role.Role
        );
        return TypedResults.Ok(user.Value.MapFromDomain());
    }
}
