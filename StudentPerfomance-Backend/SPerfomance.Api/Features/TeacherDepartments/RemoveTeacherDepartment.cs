using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.RemoveTeachersDepartment;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class RemoveTeacherDepartment
{
    public record Request(GetDepartmentByNameQuery Department);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("RemoveTeacherDepartments")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет кафедру из системы")
                        .AppendLine("Результат ОК (200): Кафедра с измененным названием.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Кафедра не найдена.")
                        .ToString()
                );
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeachersDepartmentDto>
        >
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        logger.LogInformation("Запрос на удаление кафедры");
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var department = await queryDispatcher.Dispatch<
            GetDepartmentByNameQuery,
            TeachersDepartments
        >(request.Department, ct);

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на удаление кафедры отменен причина {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.NotFound(department.Error.Description);
        }

        department = await commandDispatcher.Dispatch<
            RemoveTeachersDepartmentCommand,
            TeachersDepartments
        >(new RemoveTeachersDepartmentCommand(department.Value), ct);
        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на удаление кафедры отменен причина {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.BadRequest(department.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} удаляет кафедру {dname}",
            jwtToken.UserId,
            request.Department.Name
        );
        return TypedResults.Ok(department.Value.MapFromDomain());
    }
}
