using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class ChangeTeacherDepartmentName
{
    public record Request(GetDepartmentByNameQuery Initial, TeacherDepartmentContract Updated);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("ChangeDepartmentName")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет название кафедры")
                        .AppendLine("Результат ОК (200): Кафедра с измененным названием.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Кафедра не найдена.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeachersDepartmentDto>
        >
    > Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на изменение названия кафедры");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var department = await queryDispatcher.Dispatch<
            GetDepartmentByNameQuery,
            TeachersDepartments
        >(request.Initial, ct);

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на изменение названия кафедры отменён. Причина: {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.NotFound(department.Error.Description);
        }

        department = await commandDispatcher.Dispatch<
            ChangeTeachersDepartmentNameCommand,
            TeachersDepartments
        >(new ChangeTeachersDepartmentNameCommand(department.Value, request.Updated.Name), ct);

        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на изменение названия кафедры отменён. Причина: {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.BadRequest(department.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} изменяет название кафедры с {oldName} на {newName}",
            jwtToken.UserId,
            request.Initial.Name,
            request.Updated.Name
        );
        return TypedResults.Ok(department.Value.MapFromDomain());
    }
}
