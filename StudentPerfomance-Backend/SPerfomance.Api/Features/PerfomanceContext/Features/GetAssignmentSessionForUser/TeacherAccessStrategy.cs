using Microsoft.AspNetCore.Http.HttpResults;
using SPerfomance.Api.Features.Common.Extensions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;
using SPerfomance.Domain.Models.Teachers.Errors;
using SPerfomance.Domain.Models.Users.Errors;

namespace SPerfomance.Api.Features.PerfomanceContext.Features.GetAssignmentSessionForUser;

public sealed class TeacherAccessStrategy : IGetAssignmentSessionForUserStrategy
{
    private readonly Token _token;
    private readonly IUsersRepository _users;
    private readonly IAssignmentSessionsRepository _sessions;
    private readonly CancellationToken _ct;

    public TeacherAccessStrategy(
        string? token,
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        CancellationToken ct = default
    )
    {
        _users = users;
        _sessions = sessions;
        _ct = ct;
        _token = new Token(token);
    }

    public async Task<
        Results<
            UnauthorizedHttpResult,
            NotFound<string>,
            BadRequest<string>,
            Ok<TeacherAssignmentSession>
        >
    > GetAssignmentSessions()
    {
        if (!await _token.IsVerifiedTeacher(_users, _ct))
            return TypedResults.Unauthorized();
        var user = await _users.GetById(_token.UserId, _ct);
        if (user == null)
            return TypedResults.NotFound(UserErrors.NotFound().Description);
        var teacher = await _users.GetTeacherByUser(user, _ct);
        if (teacher == null)
            return TypedResults.NotFound(TeacherErrors.NotFound().Description);
        var session = await _sessions.GetAssignmentSessionForTeacher(teacher, _ct);
        if (session == null)
            return TypedResults.BadRequest(TeacherErrors.NotFound().Description);
        return TypedResults.Ok(session);
    }
}
