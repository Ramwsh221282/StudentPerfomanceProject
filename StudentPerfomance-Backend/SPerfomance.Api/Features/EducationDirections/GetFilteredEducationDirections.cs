using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class GetFilteredEducationDirections
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationDirectionTags.Api}/filter", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("GetPagedFilteredEducationDirections")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает коллекцию отфильтрованных направлений подготовки постранично"
                        )
                        .AppendLine(
                            "Результат ОК (200): Коллекция отфильтрованных направлений подготовки постранично."
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
        [FromQuery(Name = "filterCode")] string? filterCode,
        [FromQuery(Name = "filterName")] string? filterName,
        [FromQuery(Name = "filterType")] string? filterType,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var directions = await repository.GetPagedFiltered(
            filterCode,
            filterName,
            filterType,
            page,
            pageSize,
            ct
        );

        return TypedResults.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
