using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Models.Users.Errors;

namespace SPerfomance.Api.Features.PerfomanceContext.Features;

public static class GetAssignmentSessionForTeacher
{
    public record Request(TokenContract Token);

    public sealed class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost($"{PerfomanceContextTags.SessionsApp}/teacher-assignments-info", Handler)
                .WithTags($"{PerfomanceContextTags.SessionsTag}");
    }

    public static async Task<IResult> Handler(
        Request request,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        CancellationToken ct
    )
    {
        Token token = request.Token;
        if (!await new UserVerificationService(users).IsVerified(token, UserRole.Teacher, ct))
            return Results.BadRequest(UserTags.UnauthorizedError);

        var user = await users.GetById(token.UserId, ct);
        if (user == null)
            return Results.BadRequest(UserErrors.NotFound().Description);

        var teacher = await users.GetTeacherByUser(user, ct);
        if (teacher == null)
            return Results.BadRequest(TeacherErrors.NotFound().Description);

        var session = await sessions.GetAssignmentSessionForTeacher(teacher, ct);
        return session == null
            ? Results.BadRequest(TeacherErrors.NotFound().Description)
            : Results.Ok(session);
    }
}
