using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class RegisterStudentGroup
{
    public record Request(StudentGroupContract Group);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{StudentGroupTags.Api}", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("RegisterStudentGroup")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет студенческую группу")
                        .AppendLine("Результат ОК (200): Студенческая группа.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentGroupDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        Request request,
        ICommandDispatcher dispatcher,
        IUsersRepository users,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var group = await dispatcher.Dispatch<CreateStudentGroupCommand, StudentGroup>(
            new CreateStudentGroupCommand(request.Group.Name),
            ct
        );

        return group.IsFailure
            ? TypedResults.BadRequest(group.Error.Description)
            : TypedResults.Ok(group.Value.MapFromDomain());
    }
}
