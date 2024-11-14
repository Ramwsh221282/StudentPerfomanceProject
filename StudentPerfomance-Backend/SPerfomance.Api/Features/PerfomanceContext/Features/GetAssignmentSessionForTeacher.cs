using SPerfomance.Api.Endpoints;
using SPerfomance.Api.Features.Common;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.Teachers;
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
        IAssignmentSessionsRepository sessions
    )
    {
        Token token = request.Token;
        if (!await new UserVerificationService(users).IsVerified(token, UserRole.Teacher))
            return Results.BadRequest(UserTags.UnauthorizedError);

        User? user = await users.GetById(token.UserId);
        if (user == null)
            return Results.BadRequest(UserErrors.NotFound().Description);

        Teacher? teacher = await users.GetTeacherByUser(user);
        if (teacher == null)
            return Results.BadRequest(TeacherErrors.NotFound().Description);

        TeacherAssignmentSession? session = await sessions.GetAssignmentSessionForTeacher(teacher);
        return session == null
            ? Results.BadRequest(TeacherErrors.NotFound().Description)
            : Results.Ok(session);
    }
}
