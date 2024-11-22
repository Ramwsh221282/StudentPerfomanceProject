using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Api.Features.Teachers.Contracts;
using SPerfomance.Application.Departments.Commands.FireTeacher;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers.Abstractions;

namespace SPerfomance.Api.Features.Teachers;

public static class FireTeacherFromDepartment
{
    public record Request(
        TeacherDepartmentContract Department,
        TeacherContract Teacher,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{TeacherTags.Api}", Handler).WithTags(TeacherTags.Tag);
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
                request.Teacher.Name,
                request.Teacher.Surname,
                request.Teacher.Patronymic,
                request.Teacher.Job,
                request.Teacher.State
            ),
            ct
        );

        teacher = await new FireTeacherCommandHandler(teachers).Handle(
            new FireTeacherCommand(teacher.Value),
            ct
        );
        return teacher.IsFailure
            ? Results.BadRequest(teacher.Error.Description)
            : Results.Ok(teacher.Value.MapFromDomain());
    }
}
