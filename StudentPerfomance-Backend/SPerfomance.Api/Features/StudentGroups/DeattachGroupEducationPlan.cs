using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.DeattachEducationPlan;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class DeattachGroupEducationPlan
{
    public record Request(GetStudentGroupQuery Group);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}/deattach-education-plan", Handler)
                .WithTags($"{StudentGroupTags.Tag}")
                .WithOpenApi()
                .WithName("DeattachGroupEducationPlan")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Открепляет учебный план от группы")
                        .AppendLine(
                            "Результат ОК (200): Возвращает студенческую группу с открепленным учебным планом."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine(
                            "Результат Ошибки (404): Студенческая группа или учебный план не найдены"
                        )
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentGroupDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        IUsersRepository users,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на открепление учебного плана у группы");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Group,
            ct
        );

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на открепление учебного плана у группы отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.NotFound(group.Error.Description);
        }

        group = await commandDispatcher.Dispatch<DeattachEducationPlanCommand, StudentGroup>(
            new DeattachEducationPlanCommand(group.Value),
            ct
        );

        if (group.IsFailure)
        {
            logger.LogError(
                "Запрос пользователя {id} на открепление учебного плана у группы отменен. Причина: {text}",
                jwtToken.UserId,
                group.Error.Description
            );
            return TypedResults.BadRequest(group.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} открепляет учебный план у группы {gname}",
            jwtToken.UserId,
            request.Group.Name
        );
        return TypedResults.Ok(group.Value.MapFromDomain());
    }
}
