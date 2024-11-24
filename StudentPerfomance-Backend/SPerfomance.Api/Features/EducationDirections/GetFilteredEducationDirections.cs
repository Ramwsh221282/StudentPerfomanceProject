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
                .RequireRateLimiting("fixed")
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
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        logger.LogInformation("Запрос на фильтрацию списка направлений подготовки");
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var directions = await repository.GetPagedFiltered(
            filterCode,
            filterName,
            filterType,
            page,
            pageSize,
            ct
        );
        logger.LogInformation(
            "Получены отфильтрованные направления подготовки в количестве {count}",
            directions.Count
        );
        return TypedResults.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
