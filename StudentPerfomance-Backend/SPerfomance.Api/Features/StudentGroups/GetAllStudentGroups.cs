using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class GetAllStudentGroups
{
    public record Request(TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentGroupTags.Api}/all", Handler).WithTags(StudentGroupTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IStudentGroupsRepository repository,
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

        var groups = await repository.GetAll(ct);
        return Results.Ok(groups.Select(g => g.MapFromDomain()));
    }
}
