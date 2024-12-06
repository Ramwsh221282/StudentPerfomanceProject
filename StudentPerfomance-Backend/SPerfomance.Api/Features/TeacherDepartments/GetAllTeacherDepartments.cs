using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class GetAllTeacherDepartments
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{TeacherDepartmentsTags.Api}/all", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("GetAllTeacherDepartments")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает все кафедры")
                        .AppendLine("Результат ОК (200): Кафедры.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<Results<UnauthorizedHttpResult, Ok<TeachersDepartmentDto[]>>> Handler(
        [FromHeader(Name = "token")] string? token,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на получение всех преподавателей");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var departments = await repository.GetAll(ct);
        var result = departments.Select(d => d.MapFromDomain()).ToArray();
        logger.LogInformation(
            "Пользователь {id} получает все кафедры {count}",
            jwtToken.UserId,
            result.Length
        );
        return TypedResults.Ok(result);
    }
}
