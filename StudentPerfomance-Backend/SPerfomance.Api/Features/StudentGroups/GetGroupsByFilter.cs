using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class GetGroupsByFilter
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentGroupTags.Api}/filter", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("GetGroupsByFilter")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает отфильтрованные студенческие группы постранично"
                        )
                        .AppendLine(
                            "Результат ОК (200): Возвращает студенческую группу с измененным названием."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<StudentGroupDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        [FromQuery(Name = "groupName")] string? groupName,
        IUsersRepository users,
        IStudentGroupsRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var groups = await repository.FilterPaged(groupName, page, pageSize, ct);
        return TypedResults.Ok(groups.Select(g => g.MapFromDomain()));
    }
}
