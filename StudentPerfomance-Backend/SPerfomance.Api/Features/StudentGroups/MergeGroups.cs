using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups;

public static class MergeGroups
{
    public record Request(
        StudentGroupContract Initial,
        StudentGroupContract Target,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}/merge", Handler).WithTags(StudentGroupTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
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

        var initial = await new GetStudentGroupQueryHandler(repository).Handle(request.Initial, ct);
        var target = await new GetStudentGroupQueryHandler(repository).Handle(request.Target, ct);

        var result = await new MergeWithGroupCommandHandler(repository).Handle(
            new MergeWithGroupCommand(initial.Value, target.Value),
            ct
        );
        return result.IsFailure
            ? Results.BadRequest(result.Error.Description)
            : Results.Ok(result.Value.MapFromDomain());
    }
}
