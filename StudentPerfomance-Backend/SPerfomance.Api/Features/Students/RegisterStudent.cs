using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.AddStudentCommand;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students;

namespace SPerfomance.Api.Features.Students;

public static class RegisterStudent
{
    public record Request(StudentContract Student);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentTags.Api}", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("RegisterStudent")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет студента в группу")
                        .AppendLine("Результат ОК (200): Добавленный студент.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на создание студента в группе");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            new GetStudentGroupQuery(request.Student.GroupName),
            ct
        );

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на создание студента отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        var student = await commandDispatcher.Dispatch<AddStudentCommand, Student>(
            new AddStudentCommand(
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook,
                group.Value
            ),
            ct
        );

        if (student.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на создание студента отменен. Причина: {text}",
                jwtToken.UserId,
                student.Error.Description
            );
            return TypedResults.BadRequest(student.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} добавляет студента {sname} {ssurname} {spatronymic}, {srecordbook}, {state} в группу {gname}",
            jwtToken.UserId,
            request.Student.Name,
            request.Student.Surname,
            request.Student.Patronymic,
            request.Student.Recordbook,
            request.Student.State,
            request.Student.GroupName
        );
        return TypedResults.Ok(student.Value.MapFromDomain());
    }
}
