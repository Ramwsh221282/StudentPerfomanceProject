using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class GetPagedTeacherDepartments
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("GetPagedTeacherDepartments")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает список кафедр постранично")
                        .AppendLine("Результат ОК (200): Список кафедр постранично.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<TeachersDepartmentDto[]>>> Handler(
        [FromHeader(Name = "token")] string? token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение кафедр постранично");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogInformation("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var departments = await repository.GetPaged(page, pageSize, ct);
        var result = departments.Select(d => d.MapFromDomain()).ToArray();
        logger.LogInformation(
            "Пользователь {id} получает кафедры постранично: {count}",
            jwtToken.UserId,
            result.Length
        );
        return TypedResults.Ok(result);
    }
}
