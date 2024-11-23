using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Services.AssignmentSessionViewServices.Handlers;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class CloseAssignmentSession
{
    public record Request(string Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{PerfomanceContextTags.SessionsApi}/close-session", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("CloseAssignmentSession")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод закрывает контрольную неделю. Создаёт отчёт в системе. Удаляет сессию контрольной недели."
                        )
                        .AppendLine(
                            "Результат ОК (200): Контрольная неделя закрыта. Отчёт сформирован. Сессия контрольной недели удалена."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Сессия контрольной недели не найдена")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<string>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        IControlWeekReportRepository reports,
        Request request,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var command = new CloseAssignmentSessionCommand(request.Id);
        var session = await commandDispatcher.Dispatch<
            CloseAssignmentSessionCommand,
            AssignmentSession
        >(command, ct);

        if (session.IsFailure)
            return TypedResults.BadRequest(session.Error.Description);

        var factory = new AssignmentSessionViewFactory(session.Value);
        var view = factory.CreateView();
        var insertion = await reports.Insert(view, ct);
        return insertion.IsFailure
            ? TypedResults.BadRequest(insertion.Error.Description)
            : TypedResults.Ok("Closed");
    }
}
