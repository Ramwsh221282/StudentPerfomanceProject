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
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возаращает пользователей постранично")
                        .AppendLine("Результат ОК (200): Список пользователей.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<IEnumerable<UserDto>>>> Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(repository, ct))
            return TypedResults.Unauthorized();
        var users = await repository.GetPaged(page, pageSize, ct);
        return TypedResults.Ok(users.Select(u => u.MapFromDomain()));
    }
}
