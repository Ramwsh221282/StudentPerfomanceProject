using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class FilterTeacherDepartments
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{TeacherDepartmentsTags.Api}/filter", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("FilterTeacherDepartments")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отфильтрованные кафедры постранично")
                        .AppendLine("Результат ОК (200): Отфильтрованные кафедры постранично.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<TeachersDepartmentDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "pageSize")] int pageSize,
        [FromQuery(Name = "filterName")] string? filterName,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var departments = await repository.GetPagedFiltered(filterName, page, pageSize, ct);
        return TypedResults.Ok(departments.Select(d => d.MapFromDomain()));
    }
}
