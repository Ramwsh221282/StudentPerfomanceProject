using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Users.DTO;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersByFilter
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{UserTags.Api}/filter", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("GetUsersByFilter")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод фильтрует пользователей постранично")
                        .AppendLine(
                            "Результат ОК (200): Возвращает список отфильтрованных польззователей."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<UserDto[]>>> Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        [FromQuery(Name = "name")] string? name,
        [FromQuery(Name = "surname")] string? surname,
        [FromQuery(Name = "patronymic")] string? patronymic,
        [FromQuery(Name = "email")] string? email,
        [FromQuery(Name = "role")] string? role,
        IUsersRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение пользователей системы постранично по фильтру");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(repository, ct))
        {
            logger.LogInformation("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var users = await repository.GetFiltered(
            name,
            surname,
            patronymic,
            email,
            role,
            page,
            pageSize,
            ct
        );
        var result = users.Select(u => u.MapFromDomain()).ToArray();
        logger.LogInformation(
            "Пользователь {id} получает пользователей системы постранично по фильтру {count}",
            jwtToken.UserId,
            result.Length
        );
        return TypedResults.Ok(result);
    }
}
