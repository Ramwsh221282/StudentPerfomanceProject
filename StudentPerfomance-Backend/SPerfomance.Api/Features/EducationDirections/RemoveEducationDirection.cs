using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Tools;

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
        IEducationDirectionRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        Result<EducationDirection> direction = await new GetEducationDirectionQueryHandler(
            repository
        ).Handle(request.Direction);

        direction = await new RemoveEducationDirectionCommandHandler(repository).Handle(
            new(direction.Value)
        );

        return direction.IsFailure
            ? Results.BadRequest(direction.Error.Description)
            : Results.Ok(direction.Value.MapFromDomain());
    }
}
