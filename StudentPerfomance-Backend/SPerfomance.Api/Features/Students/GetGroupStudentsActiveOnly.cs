using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students.ValueObjects;

namespace SPerfomance.Api.Features.Students;

public static class GetGroupStudentsActiveOnly
{
    public record Request(StudentGroupContract Group, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentTags.Api}/by-group-active-only", Handler)
                .WithTags($"{StudentTags.Tag}");
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IStudentGroupsRepository groups,
        IUsersRepository users,
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

        var group = await new GetStudentGroupQueryHandler(groups).Handle(request.Group, ct);
        if (group.IsFailure)
            return Results.BadRequest(group.Error.Description);

        return Results.Ok(
            group
                .Value.Students.Where(s => s.State == StudentState.Active)
                .Select(s => s.MapFromDomain())
        );
    }
}
