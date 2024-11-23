using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Api.Features.StudentGroups.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.ChangeGroupName;
using SPerfomance.Application.StudentGroups.DTO;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.Domain.Models.StudentGroups;

namespace SPerfomance.Api.Features.StudentGroups;

public static class ChangeGroupName
{
    public record Request(GetStudentGroupQuery Initial, StudentGroupContract Updated);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPut($"{StudentGroupTags.Api}", Handler)
                .WithTags(StudentGroupTags.Tag)
                .WithOpenApi()
                .WithName("ChangeGroupName")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод изменяет название группы")
                        .AppendLine(
                            "Результат ОК (200): Возвращает студенческую группу с измененным названием."
                        )
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Студенческая группа не найдена")
                        .ToString()
                );
    }

    public static async Task<
        Results<UnauthorizedHttpResult, NotFound<string>, BadRequest<string>, Ok<StudentGroupDto>>
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
            return TypedResults.BadRequest(UserTags.UnauthorizedError);

        var group = await queryDispatcher.Dispatch<GetStudentGroupQuery, StudentGroup>(
            request.Initial,
            ct
        );

        if (group.IsFailure)
            return TypedResults.NotFound(group.Error.Description);

        group = await commandDispatcher.Dispatch<ChangeGroupNameCommand, StudentGroup>(
            new ChangeGroupNameCommand(group.Value, request.Updated.Name),
            ct
        );

        return group.IsFailure
            ? TypedResults.BadRequest(group.Error.Description)
            : TypedResults.Ok(group.Value.MapFromDomain());
    }
}
