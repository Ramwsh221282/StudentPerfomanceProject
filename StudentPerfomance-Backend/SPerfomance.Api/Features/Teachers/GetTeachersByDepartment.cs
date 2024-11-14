using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.Api.Features.Teachers;

public static class GetTeachersByDepartment
{
    public record Request(TeacherDepartmentContract Department, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{TeacherTags.Api}/by-department", Handler).WithTags(TeacherTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository
    )
    {
        if (
            !await new UserVerificationService(users).IsVerified(
                request.Token,
                UserRole.Administrator
            )
        )
            return Results.BadRequest(UserTags.UnauthorizedError);

        Result<TeachersDepartments> department = await new GetDepartmentByNameQueryHandler(
            repository
        ).Handle(request.Department);
        if (department.IsFailure)
            return Results.BadRequest(department.Error.Description);
        return Results.Ok(department.Value.Teachers.Select(t => t.MapFromDomain()));
    }
}
