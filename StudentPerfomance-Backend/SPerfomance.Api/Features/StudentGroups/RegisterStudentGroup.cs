using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class RegisterStudentGroup
{
    public record Request(StudentGroupContract Group);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentGroupTags.Api}", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("RegisterStudentGroup")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет студенческую группу")
                        .AppendLine("Результат ОК (200): Студенческая группа.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentGroupDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        HasActiveAssignmentSessionRequestHandler guard,
        Request request,
        ICommandDispatcher dispatcher,
        IUsersRepository users,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        HasActiveAssignmentSessionResponse response = await guard.Handle(
            new HasActiveAssignmentSessionRequest()
        );
        if (response.Has)
            return TypedResults.BadRequest("Запрос отклонён. Причина: Активная контрольная неделя");

        logger.LogInformation("Запрос на создание студенческой группы");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var group = await dispatcher.Dispatch<CreateStudentGroupCommand, StudentGroup>(
            new CreateStudentGroupCommand(request.Group.Name),
            ct
        );

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на создание группы отменён. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.BadRequest(group.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} создает группу {gname}",
            jwtToken.UserId,
            request.Group.Name
        );
        return TypedResults.Ok(group.Value.MapFromDomain());
    }
}
