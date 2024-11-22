using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.Students;

public static class FilterStudents
{
    public record Request(StudentContract Student, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentTags.Api}/filter", Handler).WithTags(StudentTags.Tag);
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
            new GetStudentGroupQuery(request.Student.GroupName),
            ct
        );
        if (group.IsFailure)
            return Results.BadRequest(group.Error.Description);

        return Results.Ok(
            group
                .Value.Students.Where(s =>
                    !string.IsNullOrWhiteSpace(request.Student.Name)
                        && s.Name.Name.Contains(request.Student.Name)
                    || !string.IsNullOrWhiteSpace(request.Student.Surname)
                        && s.Name.Surname.Contains(request.Student.Surname)
                    || !string.IsNullOrWhiteSpace(request.Student.Patronymic)
                        && s.Name.Patronymic.Contains(request.Student.Patronymic)
                    || !string.IsNullOrWhiteSpace(request.Student.State)
                        && s.State.State.Contains(request.Student.State)
                    || request.Student.Recordbook != null
                        && s.Recordbook.Recordbook == request.Student.Recordbook.Value
                )
                .Select(s => s.MapFromDomain())
        );
    }
}
