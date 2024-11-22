using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.EducationDirections.Commands.UpdateEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class UpdateEducationDirection
{
    public record Request(
        EducationDirectionContract Initial,
        EducationDirectionContract Updated,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{EducationDirectionTags.Api}", Handler)
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
            request.Initial,
            ct
        );

        direction = await new UpdateEducationDirectionCommandHandler(repository).Handle(
            new UpdateEducationDirectionCommand(
                direction.Value,
                request.Updated.Name,
                request.Updated.Code,
                request.Updated.Type
            ),
            ct
        );

        return direction.IsFailure
            ? Results.BadRequest(direction.Error.Description)
            : Results.Ok(direction.Value.MapFromDomain());
    }
}
