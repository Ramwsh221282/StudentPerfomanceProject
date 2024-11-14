using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudentGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.StudentGroups;

public static class RemoveStudentGroup
{
    public record Request(StudentGroupContract Group, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{StudentGroupTags.Api}", Handler).WithTags(StudentGroupTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        IStudentGroupsRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        Result<StudentGroup> group = await new GetStudentGroupQueryHandler(repository).Handle(
            request.Group
        );
        group = await new RemoveStudentGroupCommandHandler(repository).Handle(new(group.Value));

        return group.IsFailure
            ? Results.BadRequest(group.Error.Description)
            : Results.Ok(group.Value.MapFromDomain());
    }
}
