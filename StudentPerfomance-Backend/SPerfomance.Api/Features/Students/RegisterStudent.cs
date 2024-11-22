using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.StudentGroups.Commands.AddStudentCommand;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Models.Students.Errors;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Students;

public static class RegisterStudent
{
    public record Request(StudentContract Student, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentTags.Api}", Handler).WithTags(StudentTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IStudentsRepository students,
        IStudentGroupsRepository groups,
        CancellationToken ct
    )
    {
        if (await students.HasWithRecordbook(request.Student.Recordbook.ValueOrEmpty(), ct))
            return Results.BadRequest(
                StudentErrors
                    .RecordbookDublicate(request.Student.Recordbook.ValueOrEmpty())
                    .Description
            );

        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator,
                ct
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        var group = await groups.Get(request.Student.GroupName.ValueOrEmpty(), ct);
        var student = await new AddStudentCommandHandler(students).Handle(
            new AddStudentCommand(
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook,
                group
            ),
            ct
        );

        return student.IsFailure
            ? Results.BadRequest(student.Error.Description)
            : Results.Ok(student.Value.MapFromDomain());
    }
}
