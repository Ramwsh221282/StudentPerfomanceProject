using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.EducationDirections.Contracts;
using SPerfomance.Application.EducationDirections.DTO;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections;

public static class GetFilteredEducationDirections
{
    public record Request(
        EducationDirectionContract Direction,
        PaginationContract Pagination,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{EducationDirectionTags.Api}/filter", Handler)
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
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        IReadOnlyCollection<EducationDirection> directions = await repository.GetPagedFiltered(
            request.Direction.Code,
            request.Direction.Name,
            request.Direction.Type,
            request.Pagination.Page,
            request.Pagination.PageSize
        );

        return Results.Ok(directions.Select(d => d.MapFromDomain()));
    }
}
