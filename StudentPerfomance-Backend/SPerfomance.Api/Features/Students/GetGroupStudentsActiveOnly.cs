using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.Students.ValueObjects;

namespace SPerfomance.Api.Features.Students;

public static class GetGroupStudentsActiveOnly
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentTags.Api}/active-only", Handler)
                .WithTags($"{StudentTags.Tag}")
                .WithOpenApi()
                .WithName("GetActiveStudentsOnly")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет название группы")
                        .AppendLine(
                            "Результат ОК (200): Возвращает студенческую группу с измененным названием."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, Ok<StudentDto[]>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "groupName")] string groupName,
        IQueryDispatcher dispatcher,
        IUsersRepository users,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение только активных студентов группы");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var group = await dispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            new GetStudentGroupQuery(groupName),
            ct
        );
        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на получение только активных студентов группы отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        var students = group
            .Value.Students.Where(s => s.State == StudentState.Active)
            .Select(s => s.MapFromDomain())
            .ToArray();
        logger.LogInformation(
            "Пользователь {id} получает только активных студентов группы {gname} {count}",
            jwtToken.UserId,
            groupName,
            students.Length
        );
        return TypedResults.Ok(students);
    }
}
