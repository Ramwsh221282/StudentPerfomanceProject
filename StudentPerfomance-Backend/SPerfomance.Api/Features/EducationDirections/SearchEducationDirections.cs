using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class SearchEducationDirections
{
    public record Request(EducationDirectionContract Direction, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationDirectionTags.Api}/search", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("SearchEducationDirections")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает отфильтрованную коллекцию направлений подготовки из всех направлений"
                        )
                        .AppendLine(
                            "Результат ОК (200): Коллекция отфильтрованных направлений подготовки."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<EducationDirectionDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "searchCode")] string? searchCode,
        [FromQuery(Name = "searchType")] string? searchType,
        [FromQuery(Name = "searchName")] string? searchName,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var directions = await repository.GetFiltered(searchCode, searchName, searchType, ct);
        return TypedResults.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
