using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Departments.Commands.UpdateTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers.Abstractions;

namespace SPerfomance.Api.Features.Teachers;

public static class UpdateTeacher
{
    public record Request(
        TeacherDepartmentContract Department,
        TeacherContract Initial,
        TeacherContract Updated,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{TeacherTags.Api}", Handler).WithTags(TeacherTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        ITeacherDepartmentsRepository departments,
        ITeachersRepository teachers,
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

        var department = await new GetDepartmentByNameQueryHandler(departments).Handle(
            request.Department,
            ct
        );

        var teacher = await new GetTeacherFromDepartmentQueryHandler().Handle(
            new GetTeacherFromDepartmentQuery(
                department.Value,
                request.Initial.Name,
                request.Initial.Surname,
                request.Initial.Patronymic,
                request.Initial.Job,
                request.Initial.State
            ),
            ct
        );

        teacher = await new UpdateTeacherCommandHandler(teachers).Handle(
            new UpdateTeacherCommand(
                teacher.Value,
                request.Updated.Name,
                request.Updated.Surname,
                request.Updated.Patronymic,
                request.Updated.Job,
                request.Updated.State
            ),
            ct
        );

        return teacher.IsFailure
            ? Results.BadRequest(teacher.Error.Description)
            : Results.Ok(teacher.Value.MapFromDomain());
    }
}
