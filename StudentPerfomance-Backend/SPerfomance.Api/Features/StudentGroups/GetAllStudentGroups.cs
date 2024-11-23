using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class GetAllStudentGroups
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentGroupTags.Api}/all", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("GetAllStudentGroups")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает все студенческие группы")
                        .AppendLine("Результат ОК (200): Список студенческих групп.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<StudentGroupDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IStudentGroupsRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var groups = await repository.GetAll(ct);
        return TypedResults.Ok(groups.Select(g => g.MapFromDomain()));
    }
}
