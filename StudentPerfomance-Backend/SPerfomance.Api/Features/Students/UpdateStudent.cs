using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.StudentGroups.Commands.UpdateStudent;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students.Abstractions;

namespace SPerfomance.Api.Features.Students;

public static class UpdateStudent
{
    public record Request(StudentContract Student, StudentContract Updated, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentTags.Api}", Handler).WithTags(StudentTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        IStudentGroupsRepository groups,
        IStudentsRepository students,
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

        var group = await new GetStudentGroupQueryHandler(groups).Handle(
            new GetStudentGroupQuery(request.Student.GroupName),
            ct
        );
        var student = await new GetStudentFromGroupQueryHandler().Handle(
            new GetStudentFromGroupQuery(
                group.Value,
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook
            ),
            ct
        );

        student = await new UpdateStudentCommandHandler(students, groups).Handle(
            new UpdateStudentCommand(
                student.Value,
                request.Updated.Name,
                request.Updated.Surname,
                request.Updated.Patronymic,
                request.Updated.State,
                request.Updated.Recordbook,
                request.Updated.GroupName
            ),
            ct
        );

        return student.IsFailure
            ? Results.BadRequest(student.Error.Description)
            : Results.Ok(student.Value.MapFromDomain());
    }
}
