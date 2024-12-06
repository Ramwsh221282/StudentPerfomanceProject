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

public static class GetStudentsByGroup
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentTags.Api}", Handler)
                .WithTags(StudentTags.Tag)
                .WithOpenApi()
                .WithName("GetStudentsByGroup")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает студентов группы")
                        .AppendLine("Результат ОК (200): Список неактивных студентов.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
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
        logger.LogInformation("Запрос на получение студентов группы");
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
                "Запрос пользователя {id} на получение студенческих групп отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        var students = group.Value.Students.Select(s => s.MapFromDomain()).ToArray();
        logger.LogInformation(
            "Пользователь {id} получает студентов группы {gname} {count}",
            jwtToken.UserId,
            groupName,
            students.Length
        );
        return TypedResults.Ok(students);
    }
}
