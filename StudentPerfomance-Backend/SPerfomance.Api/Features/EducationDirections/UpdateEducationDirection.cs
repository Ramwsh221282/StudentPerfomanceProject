using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Commands.UpdateEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Api.Features.EducationDirections;

public static class UpdateEducationDirection
{
    public record Request(GetEducationDirectionQuery Initial, EducationDirectionContract Updated);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{EducationDirectionTags.Api}", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("UpdateEducationDirection")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет аттрибуты записи направления подготовки")
                        .AppendLine("Результат ОК (200): Измененная запись направления подготовки.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .ToString()
                );
    }

    public static async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<EducationDirectionDto>
        >
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IUsersRepository users,
        ICommandDispatcher commandDispatcher,
        IQueryDispatcher queryDispatcher,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не является администратором");
            return TypedResults.Unauthorized();
        }

        logger.LogInformation(
            "Пользователь {id} запрашивает направление подготовки для изменения",
            jwtToken.UserId
        );
        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Initial, ct);

        logger.LogInformation(
            "Пользователь {id} запрашивает направление подготовки для изменения",
            jwtToken.UserId
        );
        if (direction.IsFailure)
        {
            logger.LogError(
                "Пользователю {id} не удается получить направление подготовки для изменения",
                jwtToken.UserId
            );
            return TypedResults.NotFound(direction.Error.Description);
        }

        logger.LogInformation(
            "Изменяется направление подготовки {code}, {name}, {type}",
            direction.Value.Code.Code,
            direction.Value.Name.Name,
            direction.Value.Type.Type
        );
        logger.LogInformation(
            "Запрашиваемые изменения: {code}, {name}, {type}",
            request.Updated.Code,
            request.Updated.Name,
            request.Updated.Type
        );
        var updateCommand = new UpdateEducationDirectionCommand(
            direction.Value,
            request.Updated.Name,
            request.Updated.Code,
            request.Updated.Type
        );

        direction = await commandDispatcher.Dispatch<
            UpdateEducationDirectionCommand,
            EducationDirection
        >(updateCommand, ct);

        if (direction.IsFailure)
        {
            logger.LogError(
                "Пользователь {id} не может изменить направление подготовки причина: {text}",
                jwtToken.UserId,
                direction.Error.Description
            );
            return TypedResults.BadRequest(direction.Error.Description);
        }

        logger.LogInformation(
            "Пользователь {id} изменяет информацию о направлении подготовки. Новая информация {code} {name} {type}",
            jwtToken.UserId,
            direction.Value.Code.Code,
            direction.Value.Name.Name,
            direction.Value.Type.Type
        );
        return TypedResults.Ok(direction.Value.MapFromDomain());
    }
}
