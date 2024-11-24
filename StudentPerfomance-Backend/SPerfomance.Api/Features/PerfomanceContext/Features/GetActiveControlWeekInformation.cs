using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.GetInfo;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetActiveControlWeekInformation
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApp}/active-session-info", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetActiveControlWeekInformation")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает активную сессию контрольной недели")
                        .AppendLine("Результат ОК (200): Активная сессия контрольной недели.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<AssignmentSessionInfoDTO>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IQueryDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение текущих активных сессий контрольных недель");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerified(users, ct))
        {
            logger.LogError("Пользователь не авторизован");
            return TypedResults.Unauthorized();
        }

        var query = new GetAssignmentSessionInfoQuery();
        var info = await dispatcher.Dispatch<
            GetAssignmentSessionInfoQuery,
            AssignmentSessionInfoDTO
        >(query, ct);

        if (info.IsFailure)
        {
            logger.LogError(
                "Пользователь {id} не получил текущую активную контрольную неделю. Причина: {text}",
                jwtToken.UserId,
                info.Error.Description
            );
            return TypedResults.BadRequest(info.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} получает текущую активную контрольную неделю",
            jwtToken.UserId
        );
        return TypedResults.Ok(info.Value);
    }
}
