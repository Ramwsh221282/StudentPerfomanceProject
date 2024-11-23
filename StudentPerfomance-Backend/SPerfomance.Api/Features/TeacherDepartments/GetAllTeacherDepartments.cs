using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Departments.DTO;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments;

public static class GetAllTeacherDepartments
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{TeacherDepartmentsTags.Api}/all", Handler)
                .WithTags(TeacherDepartmentsTags.Tag)
                .WithOpenApi()
                .WithName("GetAllTeacherDepartments")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает все кафедры")
                        .AppendLine("Результат ОК (200): Кафедры.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, Ok<IEnumerable<TeachersDepartmentDto>>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        IUsersRepository users,
        ITeacherDepartmentsRepository repository,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();
        var departments = await repository.GetAll(ct);
        return TypedResults.Ok(departments.Select(d => d.MapFromDomain()));
    }
}
