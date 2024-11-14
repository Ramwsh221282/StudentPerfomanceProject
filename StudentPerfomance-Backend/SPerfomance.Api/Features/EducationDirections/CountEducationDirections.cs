using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class CountEducationDirections
{
    public record Request(TokenContract Contract);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationDirectionTags.Api}/count", Handler)
                .WithTags(EducationDirectionTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IEducationDirectionRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Contract,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);
        int count = await repository.Count();
        return Results.Ok(count);
    }
}
