using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Views;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features.GetAssignmentSessionForUser;

public static class GetCurrentAssignmentSession
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApi}", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("GetPagedAssignmentSessions")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает текущую активную контрольную неделю")
                        .AppendLine(
                            "Результат ОК (200): Информация о текущей активной контрольной неделе."
                        )
                        .AppendLine("Результат Ошибки (404): Активной контрольной недели нет.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound, Ok<AssignmentSessionView>>
    > Handler(
        [FromHeader(Name = "Token")] string? token,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        CancellationToken ct
    )
    {
        Token jwtToken = new Token(token);
        if (
            !await jwtToken.IsVerifiedAdmin(users, ct)
            && !await jwtToken.IsVerifiedTeacher(users, ct)
        )
            return TypedResults.Unauthorized();
        AssignmentSession? session = await sessions.GetActiveSession(ct);
        if (session == null)
            return TypedResults.NotFound();
        AssignmentSessionViewFactory factory = new AssignmentSessionViewFactory(session);
        return TypedResults.Ok(factory.CreateView());
    }
}
