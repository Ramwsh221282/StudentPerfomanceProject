using System.Text;
using Microsoft.AspNetCore.Mvc;
using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class RemoveControlWeekReport
{
    public record Request(string Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapDelete($"{PerfomanceContextTags.SessionsApi}/remove-report", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}")
                .WithOpenApi()
                .WithName("RemoveControlWeekReport")
                .RequireRateLimiting("fixed")
                .WithDescription(
                    new StringBuilder()
                        .AppendLine("Метод удаляет отчёт о контрольной неделе в системе.")
                        .AppendLine("Результат ОК (200): Отчёт удален.")
                        .AppendLine("Результат Ошибки (400): Ошибка запроса.")
                        .AppendLine("Результат Ошибки (404): Сессия контрольной недели не найдена")
                        .ToString()
                )
                .RequireCors("Frontend");
    }

    public static async Task<IResult> Handler(
        [FromHeader(Name = "token")] string? token,
        [FromBody] Request request,
        IControlWeekReportRepository reports,
        IUsersRepository users,
        ILogger<Endpoint> logger,
        CancellationToken ct
    )
    {
        logger.LogInformation("Запрос на удаление отчёта контрольной недели");
        var jwtToken = new Token(token);
        if (!await jwtToken.IsVerifiedAdmin(users, ct))
        {
            logger.LogError("Пользователь не являтся администратором");
            return TypedResults.BadRequest("Функция доступна только администраторам");
        }
        var isRemoved = await reports.Remove(request.Id, ct);
        if (!isRemoved)
        {
            logger.LogError("Запрос на удаление отчёта отменен. Отчёт не был найден");
            return TypedResults.BadRequest("Отчёт не был найден");
        }
        logger.LogInformation("Отчёт о контрольной недели id: {id} был удален", request.Id);
        return TypedResults.Ok();
    }
}
