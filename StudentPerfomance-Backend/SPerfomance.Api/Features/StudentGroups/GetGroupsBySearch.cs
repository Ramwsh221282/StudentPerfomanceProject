using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class GetGroupsBySearch
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentGroupTags.Api}/search", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("GetGroupsBySearch")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отфильтрованный список студенческих групп")
                        .AppendLine("Результат ОК (200): Список отфильтрованных групп по названию.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<StudentGroupDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "groupName")] string groupName,
        IUsersRepository users,
        IStudentGroupsRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var groups = await repository.Filter(groupName, ct);
        return TypedResults.Ok(groups.Select(g => g.MapFromDomain()));
    }
}
