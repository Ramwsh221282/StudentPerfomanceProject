using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class CountAssignmentSessions
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApi}/count", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("CountAssignmentSessions")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество контрольных недель в системе")
                        .AppendLine("Результат ОК (200): Количество контрольных недель.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var count = await sessions.Count(ct);
        return TypedResults.Ok(count);
    }
}
