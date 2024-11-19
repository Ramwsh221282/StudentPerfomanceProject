using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Api.Features.PerfomanceContext.Responses;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Statistics.DataAccess.EntityModels;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionReportById
{
    public record Request(TokenContract Token, string? Id);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/group-report-by-id", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IControlWeekReportRepository controlWeeks
    )
    {
        Token token = request.Token;
        bool isAdmin = await new UserVerificationService(users).IsVerified(
            request.Token,
            UserRole.Administrator
        );
        bool isTeacher = await new UserVerificationService(users).IsVerified(
            request.Token,
            UserRole.Teacher
        );
        if (!isAdmin && !isTeacher)
            return Results.BadRequest(
                "Просмотр отчётов доступен только администраторам или преподавателям"
            );

        if (string.IsNullOrWhiteSpace(request.Id))
            return Results.BadRequest("Не указан Id отчёта");

        if (controlWeeks is not ControlWeekRepository repository)
            return Results.Ok();

        ControlWeekReportEntity? report = await repository.GetById(Guid.Parse(request.Id));
        return report == null
            ? Results.BadRequest("Отчёт не найден")
            : Results.Ok(new ControlWeekReportDTO(report).GroupParts);
    }
}
