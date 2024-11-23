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
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository users,
        IAssignmentSessionsRepository sessionsRepository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerified(users, ct))
            return TypedResults.Unauthorized();
        var sessions = await sessionsRepository.GetPaged(page, pageSize, ct);
        var list = sessions.Select(s => new AssignmentSessionViewFactory(s).CreateView());
        return TypedResults.Ok(list);
    }
}
