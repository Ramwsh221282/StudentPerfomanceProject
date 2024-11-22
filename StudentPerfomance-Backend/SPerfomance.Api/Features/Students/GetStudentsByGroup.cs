using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.Students;

public static class GetStudentsByGroup
{
    public record Request(StudentGroupContract Group, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentTags.Api}/by-group", Handler).WithTags(StudentTags.Tag);
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

        var group = await new GetStudentGroupQueryHandler(repository).Handle(
            new GetStudentGroupQuery(request.Group.Name),
            ct
        );

        if (group.IsFailure)
            return Results.BadRequest(group.Error.Description);

        return Results.Ok(
            group.Value.Students.Select(s => s.MapFromDomain()).OrderBy(s => s.Surname)
        );
    }
}
