using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Application.StudentGroups.Commands.UpdateStudent;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Api.Features.Students;

public static class UpdateStudent
{
    public record Request(StudentContract Student, StudentContract Updated);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentTags.Api}", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("UpdateStudent")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет данные студента")
                        .AppendLine("Результат ОК (200): Измененный студент студент.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студент не найден")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<IResult> Handler(
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

        logger.LogInformation("Запрос на обновление данных студента");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var groupQuery = new GetStudentGroupQuery(request.Student.GroupName);
        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            groupQuery,
            ct
        );

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на обновление данных студента отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        var student = await queryDispatcher.Dispatch<GetStudentFromGroupQuery, Student>(
            new GetStudentFromGroupQuery(
                group.Value,
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook
            ),
            ct
        );

        if (student.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на обновление данных студента отменен. Причина: {text}",
                jwtToken.UserId,
                student.Error.Description
            );
            return TypedResults.NotFound(student.Error.Description);
        }

        student = await commandDispatcher.Dispatch<UpdateStudentCommand, Student>(
            new UpdateStudentCommand(
                student.Value,
                request.Updated.Name,
                request.Updated.Surname,
                request.Updated.Patronymic,
                request.Updated.State,
                request.Updated.Recordbook,
                request.Updated.GroupName
            ),
            ct
        );

        if (student.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на обновление данных студента отменен. Причина: {text}",
                jwtToken.UserId,
                student.Error.Description
            );
            return TypedResults.BadRequest(student.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} обновляет данные студента {sid}",
            jwtToken.UserId,
            student.Value.Id
        );
        return TypedResults.Ok(student.Value.MapFromDomain());
    }
}
