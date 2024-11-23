using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans;

public static class CountEducationPlans
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{EducationPlanTags.Api}/count", Handler)
                .WithTags(EducationPlanTags.Tag)
                .WithOpenApi()
                .WithName("CountEducationPlans")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод возвращает текущее количество направлений подготовки в системе"
                        )
                        .AppendLine("Результат ОК (200): Количество направлений подготовки.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<IResult> Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        IEducationPlansRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return Results.BadRequest(UserTags.UnauthorizedError);
        var count = await repository.Count(ct);
        return Results.Ok(count);
    }
}
