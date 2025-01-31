using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.HasActive;
using SPerfomance.Domain.Models.TeacherDepartments;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class RegisterTeacherDepartment
{
    public record Request(CreateTeachersDepartmentCommand Department);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("RegisterTeacherDepartments")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод создаёт кафедру")
                        .AppendLine("Результат ОК (200): Созданная кафедра.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<TeachersDepartmentDto>>
    > Handler(
        [FromHeader(Name = "token")] string? token,
        Request request,
        HasActiveAssignmentSessionRequestHandler guard,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        HasActiveAssignmentSessionResponse response = await guard.Handle(
            new HasActiveAssignmentSessionRequest()
        );
        if (response.Has)
            return TypedResults.BadRequest("Запрос отклонён. Причина: Активная контрольная неделя");

        var jwtToken = new Token(token);
        logger.LogInformation("Запрос на добавление кафедры");
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }
        var department = await dispatcher.Dispatch<
            CreateTeachersDepartmentCommand,
            TeachersDepartments
        >(request.Department, ct);
        if (department.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на добавление кафедры отменен. Причина: {text}",
                jwtToken.UserId,
                department.Error.Description
            );
            return TypedResults.BadRequest(department.Error.Description);
        }
        logger.LogInformation(
            "Пользователь {id} добавляет кафедру {dname}",
            jwtToken.UserId,
            request.Department.Name
        );
        return TypedResults.Ok(department.Value.MapFromDomain());
    }
}
