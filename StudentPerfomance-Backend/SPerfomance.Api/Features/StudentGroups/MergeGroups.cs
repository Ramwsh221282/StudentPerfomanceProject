using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class MergeGroups
{
    public record Request(GetStudentGroupQuery Initial, GetStudentGroupQuery Target);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}/merge", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("MergeGroups")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет студентов из 2 группы в 1 группу")
                        .AppendLine("Результат ОК (200): Возвращает студенческую группу.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, NotFound<string>, Ok<StudentGroupDto>>
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

        logger.LogInformation("Запрос на смешивание групп");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователль не является администратором");
            return TypedResults.Unauthorized();
        }

        var initial = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Initial,
            ct
        );

        if (initial.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} по смешиванию групп отменен. Причина {text}",
                jwtToken.UserId,
                initial.Error
            );
            return TypedResults.NotFound(initial.Error.Description);
        }

        var target = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Target,
            ct
        );

        if (target.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} по смешиванию групп отменен. Причина {text}",
                jwtToken.UserId,
                target.Error
            );
            return TypedResults.NotFound(target.Error.Description);
        }

        var result = await commandDispatcher.Dispatch<MergeWithGroupCommand, StudentGroup>(
            new MergeWithGroupCommand(initial.Value, target.Value),
            ct
        );

        if (result.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} по смешиванию групп отменен. Причина {text}",
                jwtToken.UserId,
                result.Error
            );
            return TypedResults.BadRequest(result.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} смешивает группу {target} с {initial}",
            jwtToken.UserId,
            request.Target.Name,
            request.Initial.Name
        );
        return TypedResults.Ok(result.Value.MapFromDomain());
    }
}
