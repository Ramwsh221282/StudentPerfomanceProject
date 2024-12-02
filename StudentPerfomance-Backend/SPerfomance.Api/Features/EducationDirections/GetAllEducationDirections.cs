using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class GetAllEducationDirections
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"{EducationDirectionTags.Api}/all", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("GetAllEducationDirections")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает все направления подготовки из базы данных.")
                        .AppendLine("Результат ОК (200): Коллекция направлений подготовки.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
        }
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<EducationDirectionDto>>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение всех направлений подготовки");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var directions = await repository.GetAll(ct);
        logger.LogInformation(
            "Получены все направления подготовки в количестве {count}",
            directions.Count
        );
        return TypedResults.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
