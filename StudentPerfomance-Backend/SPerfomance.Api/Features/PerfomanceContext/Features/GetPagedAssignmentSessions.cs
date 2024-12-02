using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetPagedAssignmentSessions
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApi}/byPage", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetPagedAssignmentSessions")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает сессии контрольных недель постранично")
                        .AppendLine("Результат ОК (200): Сессии контрольных недель постранично.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<AssignmentSessionView>>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository users,
        ILogger<Endpoint> logger,
        IAssignmentSessionsRepository sessionsRepository,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение сессий контрольных недель постранично");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerified(users, ct))
        {
            logger.LogError("Пользователь не авторизован");
            return TypedResults.Unauthorized();
        }
        var sessions = await sessionsRepository.GetPaged(page, pageSize, ct);
        var list = sessions.Select(s => new AssignmentSessionViewFactory(s).CreateView());
        logger.LogInformation(
            "Пользователь {id} получает сессии контрольных недель постранично",
            jwtToken.UserId
        );
        return TypedResults.Ok(list);
    }
}
