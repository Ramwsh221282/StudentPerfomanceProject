using Microsoft.AspNetCore.Http.HttpResults;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;
using SPerfomance.Domain.Models.Users;

namespace SPerfomance.Api.Features.PerfomanceContext.Features.GetAssignmentSessionForUser;

public sealed class AdminAccessStrategy : IGetAssignmentSessionForUserStrategy
{
    private readonly IUsersRepository _users;
    private readonly IAssignmentSessionsRepository _sessions;
    private readonly ITeachersRepository _teachers;
    private readonly string? _adminAssignmentsAccess;
    private readonly CancellationToken _ct;

    public AdminAccessStrategy(
        IUsersRepository users,
        IAssignmentSessionsRepository sessions,
        ITeachersRepository teachers,
        string? adminAssignmentsAccess,
        CancellationToken ct = default
    )
    {
        _users = users;
        _sessions = sessions;
        _teachers = teachers;
        _adminAssignmentsAccess = adminAssignmentsAccess;
        _ct = ct;
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
        if (string.IsNullOrWhiteSpace(_adminAssignmentsAccess))
            return TypedResults.BadRequest(
                "Получение доступа к оценкам из режима администратора провален"
            );

        string adminId = _adminAssignmentsAccess.Split('#')[0];
        string teacherId = _adminAssignmentsAccess.Split('#')[1];

        User? user = await _users.GetById(adminId, _ct);
        if (user == null)
            return TypedResults.NotFound("Администратор не найден");
        Teacher? teacher = await _teachers.GetById(teacherId, _ct);
        if (teacher == null)
            return TypedResults.NotFound("Преподаватель не найден");
        var session = await _sessions.GetAssignmentSessionForTeacher(teacher, _ct);
        if (session == null)
            return TypedResults.BadRequest(
                $"Нет активной контрольной недели для преподавателя {teacher.Name.Name} {teacher.Name.Surname} {teacher.Name.Patronymic}"
            );
        return TypedResults.Ok(session);
    }
}
