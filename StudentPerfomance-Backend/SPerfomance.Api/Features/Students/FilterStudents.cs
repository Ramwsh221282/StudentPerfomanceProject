using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.Students;

public static class FilterStudents
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentTags.Api}/filter", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("FilterStudents")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод фильтрует студентов в группе")
                        .AppendLine(
                            "Результат ОК (200): Возвращает отфильрованных студентов группы."
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
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "groupName")] string groupName,
        [FromQuery(Name = "name")] string? name,
        [FromQuery(Name = "surname")] string? surname,
        [FromQuery(Name = "patronymic")] string? patronymic,
        [FromQuery(Name = "state")] string? state,
        [FromQuery(Name = "recordBook")] int? recordbook,
        ILogger<Endpoint> logger,
        IUsersRepository users,
        IQueryDispatcher dispatcher,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение студентов по фильтру постранично");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var groupQuery = new GetStudentGroupQuery(groupName);
        var group = await dispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(groupQuery, ct);

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на получение студентов по фильтру постранично отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        var students = group
            .Value.Students.Where(s =>
                !string.IsNullOrWhiteSpace(name) && s.Name.Name.Contains(name)
                || !string.IsNullOrWhiteSpace(surname) && s.Name.Surname.Contains(surname)
                || !string.IsNullOrWhiteSpace(patronymic) && s.Name.Patronymic.Contains(patronymic)
                || !string.IsNullOrWhiteSpace(state) && s.State.State.Contains(state)
                || recordbook != null && s.Recordbook.Recordbook == (ulong)recordbook.Value
            )
            .Select(s => s.MapFromDomain())
            .ToArray();
        logger.LogInformation(
            "Пользователь {id} получает студентов по фильтру постранично {count}",
            jwtToken.UserId,
            students.Length
        );
        return TypedResults.Ok(students);
    }
}
