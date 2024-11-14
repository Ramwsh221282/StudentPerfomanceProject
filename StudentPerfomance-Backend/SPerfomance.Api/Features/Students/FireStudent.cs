using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.Students.Contracts;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudent;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Students;

public class FireStudent
{
    public record Request(StudentContract Student, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{StudentTags.Api}", Handler).WithTags(StudentTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        IStudentGroupsRepository groups,
        IStudentsRepository students
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
            new(request.Student.GroupName)
        );

        Result<Student> student = await new GetStudentFromGroupQueryHandler().Handle(
            new(
                group.Value,
                request.Student.Name,
                request.Student.Surname,
                request.Student.Patronymic,
                request.Student.State,
                request.Student.Recordbook
            )
        );

        student = await new RemoveStudentCommandHandler(students).Handle(new(student.Value));
        return student.IsFailure
            ? Results.BadRequest(student.Error.Description)
            : Results.Ok(student.Value.MapFromDomain());
    }
}
