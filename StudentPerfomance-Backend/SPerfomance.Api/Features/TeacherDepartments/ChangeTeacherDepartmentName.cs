using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class ChangeTeacherDepartmentName
{
    public record Request(
        TeacherDepartmentContract Initial,
        TeacherDepartmentContract Updated,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{TeacherDepartmentsTags.Api}", Handler)
                .WithTags(TeacherDepartmentsTags.Tag);
    }

    public static async Task<IResult> Handler(
        [FromBody] Request request,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository,
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

        var department = await new GetDepartmentByNameQueryHandler(repository).Handle(
            request.Initial,
            ct
        );
        department = await new ChangeTeachersDepartmentNameCommandHandler(repository).Handle(
            new ChangeTeachersDepartmentNameCommand(department.Value, request.Updated.Name),
            ct
        );

        return department.IsFailure
            ? Results.BadRequest(department.Error.Description)
            : Results.Ok(department.Value.MapFromDomain());
    }
}
