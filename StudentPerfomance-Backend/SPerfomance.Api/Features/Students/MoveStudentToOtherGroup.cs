using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Application.StudentGroups.Commands.MoveStudentToOtherGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Api.Features.Students;

public static class MoveStudentToOtherGroup
{
    public record Request(Guid StudentId, Guid CurrentGroupId, Guid OtherGroupId);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentTags.Api}/change-group", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("ChangeStudentGroup")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет группу студента")
                        .AppendLine("Результат ОК (200): Студент с измененной группой.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<StudentDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        HasActiveAssignmentSessionRequestHandler guard,
        ICommandDispatcher dispatcher,
        IUsersRepository users,
        CancellationToken ct,
        ILogger<Endpoint> logger
    )
    {
        HasActiveAssignmentSessionResponse response = await guard.Handle(
            new HasActiveAssignmentSessionRequest()
        );
        if (response.Has)
            return TypedResults.BadRequest("Запрос отклонён. Причина: Активная контрольная неделя");

        logger.LogInformation("Запрос на получение только активных студентов группы");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var command = new MoveStudentToOtherGroupCommand(
            request.StudentId,
            request.CurrentGroupId,
            request.OtherGroupId
        );
        var result = await dispatcher.Dispatch<MoveStudentToOtherGroupCommand, Student>(
            command,
            ct
        );
        if (!result.IsFailure)
            return TypedResults.Ok(result.Value.MapFromDomainWithoutGroup());
        logger.LogError("{text}", result.Error.Description);
        return TypedResults.BadRequest(result.Error.Description);
    }
}
