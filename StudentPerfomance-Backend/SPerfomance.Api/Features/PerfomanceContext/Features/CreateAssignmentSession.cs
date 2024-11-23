using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.Errors;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class CreateAssignmentSession
{
    public record Request(DateContract DateStart, DateContract DateClose);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApi}", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("CreateAssignmentSession")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод создаёт метод создает сессию контрольной недели в системе"
                        )
                        .AppendLine(
                            "Результат ОК (200): Возвращает сессию контрольной недели для проставления оценок."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<AssignmentSessionDTO>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        Request request,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        DateTime dateStart;
        DateTime dateEnd;
        try
        {
            dateStart = new DateTime(
                request.DateStart.Year.GetValueOrDefault(),
                request.DateStart.Month.GetValueOrDefault(),
                request.DateStart.Day.GetValueOrDefault()
            );

            dateEnd = new DateTime(
                request.DateClose.Year.GetValueOrDefault(),
                request.DateClose.Month.GetValueOrDefault(),
                request.DateClose.Day.GetValueOrDefault()
            );
        }
        catch
        {
            return TypedResults.BadRequest(AssignmentWeekErrors.InvalidDateFormat().Description);
        }

        var command = new CreateAssignmentSessionCommand(dateStart, dateEnd);
        var session = await dispatcher.Dispatch<CreateAssignmentSessionCommand, AssignmentSession>(
            command,
            ct
        );

        return session.IsFailure
            ? TypedResults.BadRequest(session.Error.Description)
            : TypedResults.Ok(new AssignmentSessionDTO(session.Value));
    }
}
