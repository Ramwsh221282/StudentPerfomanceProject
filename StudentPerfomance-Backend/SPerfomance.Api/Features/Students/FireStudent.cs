using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudent;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Api.Features.Students;

public class FireStudent
{
    public record Request(StudentContract Student);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{StudentTags.Api}", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("FireStudent")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет студента из группы")
                        .AppendLine("Результат ОК (200): Возвращает удаленного студента из группы.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студент не найден")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на удаление студента из группы");
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
                "Запрос пользователя {id} отменен. Причина: {text}",
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
                "Запрос пользователя {id} отменен. Причина: {text}",
                jwtToken.UserId,
                student.Error.Description
            );
            return TypedResults.NotFound(student.Error.Description);
        }

        student = await commandDispatcher.Dispatch<RemoveStudentCommand, Student>(
            new RemoveStudentCommand(student.Value),
            ct
        );

        if (student.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} отменен. Причина: {text}",
                jwtToken.UserId,
                student.Error.Description
            );
            return TypedResults.BadRequest(student.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} удаляет студента {sname} {surname} {srecordBook} из группы {gname}",
            jwtToken.UserId,
            request.Student.Name,
            request.Student.Surname,
            request.Student.Recordbook,
            request.Student.GroupName
        );
        return TypedResults.Ok(student.Value.MapFromDomain());
    }
}
