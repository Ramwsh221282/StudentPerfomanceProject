using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class CountStudentGroups
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{StudentGroupTags.Api}/count", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("CountStudentGroups")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество студенческих групп")
                        .AppendLine(
                            "Результат ОК (200): Возвращает студенческую группу с измененным названием."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IStudentGroupsRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var count = await repository.Count(ct);
        return TypedResults.Ok(count);
    }
}
