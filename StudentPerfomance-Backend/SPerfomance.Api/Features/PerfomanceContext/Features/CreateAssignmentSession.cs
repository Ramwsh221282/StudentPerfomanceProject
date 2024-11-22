using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks.Errors;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class CreateAssignmentSession
{
    public record Request(DateContract DateStart, DateContract DateClose, TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApi}", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        IStudentGroupsRepository groups,
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

        DateTime dateStart;
        DateTime dateEnd;
        try
        {
            dateStart = new DateTime(
                request.DateStart.Year.GetValueOrDefault(),
                request.DateStart.Month.GetValueOrDefault(),
                request.DateStart.Day.GetValueOrDefault()
            );

            dateEnd = new DateTime(
                request.DateClose.Year.GetValueOrDefault(),
                request.DateClose.Month.GetValueOrDefault(),
                request.DateClose.Day.GetValueOrDefault()
            );
        }
        catch
        {
            return Results.BadRequest(AssignmentWeekErrors.InvalidDateFormat().Description);
        }

        var session = await new CreateAssignmentSessionCommandHandler(sessions, groups).Handle(
            new CreateAssignmentSessionCommand(dateStart, dateEnd),
            ct
        );

        return session.IsFailure
            ? Results.BadRequest(session.Error.Description)
            : Results.Ok(new AssignmentSessionDTO(session.Value));
    }
}
