using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.Commands.DeattachEducationPlan;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.StudentGroups;

public static class DeattachGroupEducationPlan
{
    public record Request(StudentGroupContract Group, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}/deattach-education-plan", Handler)
                .WithTags($"{StudentGroupTags.Tag}");
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IStudentGroupsRepository groups,
        IUsersRepository users
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        Result<StudentGroup> group = await new GetStudentGroupQueryHandler(groups).Handle(
            request.Group
        );
        group = await new DeattachEducationPlanCommandHandler(groups).Handle(new(group.Value));
        return group.IsFailure
            ? Results.BadRequest(group.Error.Description)
            : Results.Ok(group.Value.MapFromDomain());
    }
}
