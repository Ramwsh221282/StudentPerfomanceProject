using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.TeacherDepartments.Contracts;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class FilterTeacherDepartments
{
    public record Request(
        TeacherDepartmentContract Department,
        PaginationContract Pagination,
        TokenContract Token
    );

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{TeacherDepartmentsTags.Api}/filter", Handler)
                .WithTags(TeacherDepartmentsTags.Tag);
    }

    public static async Task<IResult> Handler(
        Request request,
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

        var departments = await repository.GetPagedFiltered(
            request.Department.Name,
            request.Pagination.Page,
            request.Pagination.PageSize,
            ct
        );

        return Results.Ok(departments.Select(d => d.MapFromDomain()));
    }
}
