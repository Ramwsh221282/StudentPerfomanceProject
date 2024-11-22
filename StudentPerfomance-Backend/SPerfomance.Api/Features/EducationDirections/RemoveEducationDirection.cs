using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class RemoveEducationDirection
{
    public record Request(EducationDirectionContract Direction, TokenContract Token);

    public record Response(EducationDirectionDTO Direction);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{EducationDirectionTags.Api}", Handler)
                .WithTags(EducationDirectionTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        IEducationDirectionRepository repository,
        CancellationToken ct
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator,
                ct
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        var direction = await new GetEducationDirectionQueryHandler(repository).Handle(
            request.Direction,
            ct
        );

        direction = await new RemoveEducationDirectionCommandHandler(repository).Handle(
            new RemoveEducationDirectionCommand(direction.Value),
            ct
        );

        return direction.IsFailure
            ? Results.BadRequest(direction.Error.Description)
            : Results.Ok(direction.Value.MapFromDomain());
    }
}
