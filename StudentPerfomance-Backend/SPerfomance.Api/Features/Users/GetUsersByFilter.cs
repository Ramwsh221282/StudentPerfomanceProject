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
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод фильтрует пользователей постранично")
                        .AppendLine(
                            "Результат ОК (200): Возвращает список отфильтрованных польззователей."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<IEnumerable<UserDto>>>> Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        [FromQuery(Name = "name")] string? name,
        [FromQuery(Name = "surname")] string? surname,
        [FromQuery(Name = "patronymic")] string? patronymic,
        [FromQuery(Name = "email")] string? email,
        [FromQuery(Name = "role")] string? role,
        IUsersRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(repository, ct))
            return TypedResults.Unauthorized();

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
        return TypedResults.Ok(users.Select(u => u.MapFromDomain()));
    }
}
