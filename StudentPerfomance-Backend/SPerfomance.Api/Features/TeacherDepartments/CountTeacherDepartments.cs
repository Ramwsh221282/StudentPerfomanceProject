using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class CountTeacherDepartments
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{TeacherDepartmentsTags.Api}/count", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("CountTeacherDepartments")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает количество кафедр")
                        .AppendLine("Результат ОК (200): Количество кафедр.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<int>>> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        logger.LogInformation("Запрос на получение количества кафедр");
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var count = await repository.Count(ct);
        logger.LogInformation(
            "Пользователь {id} получает количество кафедр {count}",
            jwtToken.UserId,
            count
        );
        return TypedResults.Ok(count);
    }
}
