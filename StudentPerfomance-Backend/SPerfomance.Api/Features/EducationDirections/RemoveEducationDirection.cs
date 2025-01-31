using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Api.Features.EducationDirections;

public static class RemoveEducationDirection
{
    public record Request(GetEducationDirectionQuery Query);

    public record Response(EducationDirectionDto Direction);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{EducationDirectionTags.Api}", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("RemoveEducationDirection")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет направление подготовки из системы")
                        .AppendLine(
                            "Результат ОК (200): Удаленное направление подготовки постранично."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine(
                            "Результат Ошибки (404): Запись направления подготовки не найдена."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<EducationDirectionDto>
        >
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        HasActiveAssignmentSessionRequestHandler guard,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        HasActiveAssignmentSessionResponse response = await guard.Handle(
            new HasActiveAssignmentSessionRequest()
        );
        if (response.Has)
            return TypedResults.BadRequest("Запрос отклонён. Причина: Активная контрольная неделя");

        logger.LogInformation(
            "Запрос на удаление направления подготовки: {code}, {name}, {type}",
            request.Query.Code,
            request.Query.Name,
            request.Query.Type
        );
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        logger.LogInformation("Запрашивается удаляемое направление подготовки");
        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Query, ct);

        if (direction.IsFailure)
        {
            logger.LogError("Удаляемое направление подготовки не найдено");
            return TypedResults.NotFound(direction.Error.Description);
        }

        direction = await commandDispatcher.Dispatch<
            RemoveEducationDirectionCommand,
            EducationDirection
        >(new RemoveEducationDirectionCommand(direction), ct);

        if (direction.IsFailure)
        {
            logger.LogError(
                "Не удалось удалить направление подготовки по причине: {text}",
                direction.Error.Description
            );
            return TypedResults.BadRequest(direction.Error.Description);
        }

        logger.LogInformation(
            "Направление подготовки {code} {name} {type} было удалено",
            direction.Value.Code.Code,
            direction.Value.Name.Name,
            direction.Value.Type.Type
        );
        return TypedResults.Ok(direction.Value.MapFromDomain());
    }
}
