using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class MergeGroups
{
    public record Request(GetStudentGroupQuery Initial, GetStudentGroupQuery Target);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}/merge", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("MergeGroups")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод добавляет студентов из 2 группы в 1 группу")
                        .AppendLine("Результат ОК (200): Возвращает студенческую группу.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, BadRequest<string>, NotFound<string>, Ok<StudentGroupDto>>
    > Handler(
        [FromHeader(Name = "token")] string token,
        [FromBody] Request request,
        IUsersRepository users,
        IQueryDispatcher queryDispatcher,
        ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        if (!await new Token(token).IsVerifiedAdmin(users, ct))
            return TypedResults.Unauthorized();

        var initial = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Initial,
            ct
        );

        if (initial.IsFailure)
            return TypedResults.NotFound(initial.Error.Description);

        var target = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Target,
            ct
        );

        if (target.IsFailure)
            return TypedResults.NotFound(target.Error.Description);

        var result = await commandDispatcher.Dispatch<MergeWithGroupCommand, StudentGroup>(
            new MergeWithGroupCommand(initial.Value, target.Value),
            ct
        );

        return result.IsFailure
            ? TypedResults.BadRequest(result.Error.Description)
            : TypedResults.Ok(result.Value.MapFromDomain());
    }
}
