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
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Initial, ct);
        if (direction is null)
            return TypedResults.NotFound(direction.Error.Description);

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

        return direction.IsFailure
            ? TypedResults.BadRequest(direction.Error.Description)
            : TypedResults.Ok(direction.Value.MapFromDomain());
    }
}
