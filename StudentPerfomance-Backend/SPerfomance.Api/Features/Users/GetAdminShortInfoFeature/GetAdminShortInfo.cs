using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.Users.GetAdminShortInfoFeature.Responses;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.Users.GetAdminShortInfoFeature;

public static class GetAdminShortInfo
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{UserTags.Api}/admin-info", Handler)
                .WithTags("AdminInfo")
                .WithOpenApi()
                .WithName("GetAdminInfo")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine(
                            "Метод предоставляет краткую информацию о текущем состоянии учебных процессов"
                        )
                        .AppendLine(
                            "Результат ОК (200): Возвращает список объектов учебного процесса."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<AdminShortInfoResponse>>> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository usersRepository,
        IEducationDirectionRepository directionRepository
    )
    {
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(usersRepository))
            return TypedResults.Unauthorized();
        IReadOnlyCollection<EducationDirection> directions = await directionRepository.GetAll();
        AdminShortInfoResponse response = new AdminShortInfoResponse(directions);
        return TypedResults.Ok(response);
    }
}
