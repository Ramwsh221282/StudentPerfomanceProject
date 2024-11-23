using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections;

namespace SPerfomance.Api.Features.EducationDirections;

public static class CreateEducationDirection
{
    public record Request(CreateEducationDirectionCommand Command);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationDirectionTags.Api}", Handler)
                .WithTags(EducationDirectionTags.Tag)
                .WithOpenApi()
                .WithName("CreateEducationDirection")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод создаёт направление подготовки в системе")
                        .AppendLine("Результат ОК (200): Информация о направлении подготовки.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, Ok<EducationDirectionDto>>
    > Handler(
        Request request,
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        ICommandDispatcher dispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var direction = await dispatcher.Dispatch<
            CreateEducationDirectionCommand,
            EducationDirection
        >(request.Command, ct);

        return direction.IsFailure
            ? TypedResults.BadRequest(direction.Error.Description)
            : TypedResults.Ok(direction.Value.MapFromDomain());
    }
}
