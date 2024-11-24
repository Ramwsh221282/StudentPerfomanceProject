using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudentGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class RemoveStudentGroup
{
    public record Request(GetStudentGroupQuery Group);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{StudentGroupTags.Api}", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("RemoveStudentGroup")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет студенческую группу из системы")
                        .AppendLine("Результат ОК (200): Удаленная студенческая группа.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                );
    }

    public static async Task<IResult> Handler(
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
        logger.LogInformation("Запрос на удаление студенческой группы");
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Group,
            ct
        );

        if (group.IsFailure)
        {
            logger.LogInformation(
                "Запрос пользователя {id} на удаление группы отменен. Причина: {error}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        group = await commandDispatcher.Dispatch<RemoveStudentGroupCommand, StudentGroup>(
            new RemoveStudentGroupCommand(group.Value),
            ct
        );

        if (group.IsFailure)
        {
            logger.LogInformation(
                "Запрос пользователя {id} на удаление группы отменен. Причина: {error}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.BadRequest(group.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} удаляет группу {gname}",
            jwtToken.UserId,
            request.Group.Name
        );
        return TypedResults.Ok(group.Value.MapFromDomain());
    }
}
