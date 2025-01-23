using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Users.Commands.RegisterAsTeacher;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Teachers;

public static class RegisterTeacherAsUser
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{TeacherTags.Api}/asuser", Handler)
                .WithTags(TeacherTags.Tag)
                .WithOpenApi()
                .WithName("RegisterTeacherAsUser")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод регистрирует преподавателя в системе")
                        .AppendLine("Результат ОК (200): Преподаватель зарегистрирован.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<TeacherDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] RegisterAsTeacherRequest request,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        CancellationToken ct = default
    )
    {
        Token jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users))
            return TypedResults.Unauthorized();
        Result<Teacher> result = await dispatcher.Dispatch<RegisterAsTeacherRequest, Teacher>(
            request
        );
        return result.IsFailure
            ? TypedResults.BadRequest(result.Error.Description)
            : TypedResults.Ok(result.Value.MapFromDomain());
    }
}
