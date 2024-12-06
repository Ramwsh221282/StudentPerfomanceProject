using System.Text;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.Services.Authentication.Abstractions;
using SPerfomance.Domain.Models.Users.Errors;

namespace SPerfomance.Api.Features.Users;

public static class UsersAuth
{
    public record Request(string Email, string Password);

    public record Response(
        string Name,
        string Surname,
        string? Patronymic,
        string Email,
        string Role,
        string Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{UserTags.App}/login", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("UsersAuth")
                .RequireRateLimiting("fixed")
                .WithDescription(new StringBuilder().AppendLine("Авторизация").ToString())
                .RequireCors("Frontend");
        }
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository repository,
        IPasswordHasher hasher,
        IJwtTokenService service,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Авторизация пользователя");
        var user = await repository.GetByEmail(request.Email, ct);
        if (user == null)
        {
            logger.LogError("Ошибка авторизации. Пользователь не найден");
            return Results.NotFound(UserErrors.NotFound());
        }

        var isVerified = hasher.Verify(request.Password, user.HashedPassword);
        if (!isVerified)
        {
            logger.LogInformation("Ошибка авторизации. Пароль неверный");
            return Results.BadRequest(UserErrors.PasswordInvalid());
        }

        var token = service.GenerateToken(user);

        logger.LogInformation(
            "Авторизован пользователь {uemail} {uname} {usurname} {urole}",
            user.Email.Email,
            user.Name.Name,
            user.Name.Surname,
            user.Role.Role
        );
        return Results.Ok(
            new Response(
                user.Name.Name,
                user.Name.Surname,
                user.Name.Patronymic,
                user.Email.Email,
                user.Role.Role,
                token
            )
        );
    }
}
