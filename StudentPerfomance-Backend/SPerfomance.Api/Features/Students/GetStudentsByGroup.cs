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
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает студентов группы")
                        .AppendLine("Результат ОК (200): Список неактивных студентов.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, Ok<IEnumerable<StudentDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "groupName")] string groupName,
        IQueryDispatcher dispatcher,
        IUsersRepository users,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var group = await dispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            new GetStudentGroupQuery(groupName),
            ct
        );
        if (group.IsFailure)
            return TypedResults.NotFound(group.Error.Description);

        return TypedResults.Ok(group.Value.Students.Select(s => s.MapFromDomain()));
    }
}
