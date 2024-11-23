using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReport
{
    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet($"{PerfomanceContextTags.SessionsApi}/report", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("CreateEducationDirection")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод возвращает отчёт о контрольной неделе")
                        .AppendLine("Результат ОК (200): Возвращает отчёт о контрольной неделе.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (401): Ошибка авторизации.")
                        .AppendLine("Результат Ошибки (404): Отчёт не найден")
                        .ToString()
                );
    }

    public static async Task<IResult> Handler(
        [FromHeader(Name = "token")] string token,
        [FromQuery(Name = "id")] string id,
        IUsersRepository users,
        IControlWeekReportRepository repository
    )
    {
        if (!await )

        // return !await request.Token.IsVerifiedAdmin(users)
        //     ? Results.BadRequest(UserTags.UnauthorizedError)
        //     : Results.Ok();

        return Results.Ok();
    }
}
