using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class GetPagedEducationDirections
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationDirectionTags.Api}", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("GetPagedEducationDirections")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает коллекцию направлений подготовки постранично")
                        .AppendLine(
                            "Результат ОК (200): Коллекция направлений подготовки постранично."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<EducationDirectionDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var directions = await repository.GetPaged(page, pageSize, ct);
        return TypedResults.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
