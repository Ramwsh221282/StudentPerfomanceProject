using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;

namespace SPerfomance.Api.Features.Users;

public static class GetUsersCount
{
    public record Request(TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{UserTags.Api}/count", Handler)
                .WithTags($"{UserTags.Tag}")
                .WithOpenApi()
                .WithName("GetUsersCount")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество пользователей")
                        .AppendLine("Результат ОК (200): Количество пользователей.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(repository, ct))
            return TypedResults.Unauthorized();

        var count = await repository.Count(ct);
        return TypedResults.Ok(count);
    }
}
