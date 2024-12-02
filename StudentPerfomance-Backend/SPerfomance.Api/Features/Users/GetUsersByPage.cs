using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Users.DTO;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersByPage
{
    public record Request(PaginationContract Pagination, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{UserTags.Api}", Handler)
                .WithTags(UserTags.Tag)
                .WithOpenApi()
                .WithName("GetUsersByPage")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возаращает пользователей постранично")
                        .AppendLine("Результат ОК (200): Список пользователей.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<UserDto[]>>> Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Получение пользователей системы постранично");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(repository, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var users = await repository.GetPaged(page, pageSize, ct);
        var result = users.Select(u => u.MapFromDomain()).ToArray();
        logger.LogInformation(
            "Пользователь {id} получает список пользователей системы постранично {count}",
            jwtToken.UserId,
            result.Length
        );
        return TypedResults.Ok(result);
    }
}
