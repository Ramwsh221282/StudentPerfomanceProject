using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class CountEducationDirections
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationDirectionTags.Api}/count", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("GetEducationDirectionsCount")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает количество существующих направлений подготовки из базы данных"
                        )
                        .AppendLine("Результат ОК (200): целое число.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<Ok<int>, UnauthorizedHttpResult>> Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var count = await repository.Count(ct);
        return TypedResults.Ok(count);
    }
}
