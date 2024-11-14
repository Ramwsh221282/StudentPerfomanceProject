using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class GetAllEducationDirections
{
    public record Request(TokenContract Token);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"{EducationDirectionTags.Api}/all", Handler)
                .WithTags(EducationDirectionTags.Tag);
        }
    }

    public static async Task<IResult> Handler(
        Request request,
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

        IReadOnlyCollection<EducationDirection> directions = await repository.GetAll();
        return Results.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
