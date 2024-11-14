using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.Application.EducationPlans.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;
using SPerfomance.Domain.Models.EducationDirections.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.EducationPlans;

public static class GetEducationPlansByDirection
{
    public record Request(EducationDirectionContract Direction, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationPlanTags.Api}/by-education-direction", Handler)
                .WithTags($"{EducationPlanTags.Tag}");
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        IEducationDirectionRepository directions
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
            directions
        ).Handle(request.Direction);
        return direction == null || direction.IsFailure
            ? Results.BadRequest(EducationDirectionErrors.NotFoundError().Description)
            : Results.Ok(direction.Value.Plans.Select(p => p.MapFromDomain()));
    }
}
