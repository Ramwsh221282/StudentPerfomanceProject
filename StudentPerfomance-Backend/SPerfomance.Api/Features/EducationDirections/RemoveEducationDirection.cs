using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Api.Features.EducationDirections;

public static class RemoveEducationDirection
{
    public record Request(GetEducationDirectionQuery Query);

    public record Response(EducationDirectionDto Direction);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{EducationDirectionTags.Api}", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("RemoveEducationDirection")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет направление подготовки из системы")
                        .AppendLine(
                            "Результат ОК (200): Удаленное направление подготовки постранично."
                        )
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine(
                            "Результат Ошибки (404): Запись направления подготовки не найдена."
                        )
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
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var direction = await queryDispatcher.Dispatch<
            GetEducationDirectionQuery,
            EducationDirection
        >(request.Query, ct);

        if (direction.Value is null)
            return TypedResults.NotFound(direction.Error.Description);

        direction = await commandDispatcher.Dispatch<
            RemoveEducationDirectionCommand,
            EducationDirection
        >(new RemoveEducationDirectionCommand(direction), ct);

        return direction.IsFailure
            ? TypedResults.BadRequest(direction.Error.Description)
            : TypedResults.Ok(direction.Value.MapFromDomain());
    }
}
