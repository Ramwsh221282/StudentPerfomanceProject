using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.ValueObject;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class AssignmentSessionsRepository : IAssignmentSessionsRepository
{
    private readonly DatabaseContext _context = new();

    public async Task Insert(AssignmentSession session, CancellationToken ct = default)
    {
        await using (var transaction = await _context.Database.BeginTransactionAsync(ct))
        {
            try
            {
                var currentWeekNumbers = await GenerateWeekEntityNumber(ct);
                var currentMarkNumber = await GenerateAssignmentEntityNumber(ct);
                var currentStudentAssignmentNumber = await GenerateStudentAssignmentEntityNumber(
                    ct
                );

                session.SetNumber(await GenerateEntityNumber(ct));
                const string sessionSql = """
                    INSERT INTO Sessions(Id, SessionStartDate, SessionCloseDate, Number_Number, State_State, Type_Type, EntityNumber)
                    VALUES
                    (@Id, @StartDate, @EndDate, @Number, @State, @Type, @EntityNumber)
                    """;
                SqliteParameter[] sessionParameters =
                [
                    new SqliteParameter("@Id", session.Id),
                    new SqliteParameter("@StartDate", session.SessionStartDate),
                    new SqliteParameter("@EndDate", session.SessionCloseDate),
                    new SqliteParameter("@Number", session.Number.Number),
                    new SqliteParameter("@State", session.State.State),
                    new SqliteParameter("@Type", session.Type.Type),
                    new SqliteParameter("@EntityNumber", session.EntityNumber),
                ];
                await _context.Database.ExecuteSqlRawAsync(sessionSql, sessionParameters, ct);

                const string weekSql = """
                    INSERT INTO Weeks(Id, SessionId, GroupId, EntityNumber)
                    VALUES
                    (@Id, @SessionId, @GroupId, @EntityNumber)
                    """;

                const string assignmentSQL = """
                    INSERT INTO Assignments (Id, DisciplineId, WeekId, EntityNumber)
                    VALUES (@Id, @DisciplineId, @WeekId, @EntityNumber)
                    """;

                const string studentAssignmentSQL = """
                    INSERT INTO StudentAssignments (Id, AssignmentId, StudentId, Value_Value, EntityNumber)
                    VALUES (@Id, @AssignmentId, @StudentId, @Value_Value, @EntityNumber)
                    """;

                foreach (var week in session.Weeks)
                {
                    week.SetNumber(currentWeekNumbers);
                    SqliteParameter[] weekParameters =
                    [
                        new SqliteParameter("@Id", week.Id),
                        new SqliteParameter("@SessionId", week.Session.Id),
                        new SqliteParameter("@GroupId", week.Group!.Id),
                        new SqliteParameter("@EntityNumber", week.EntityNumber),
                    ];
                    await _context.Database.ExecuteSqlRawAsync(weekSql, weekParameters, ct);
                    currentWeekNumbers++;
                    foreach (var mark in week.Assignments)
                    {
                        mark.SetNumber(currentMarkNumber);
                        SqliteParameter[] markParameters =
                        [
                            new SqliteParameter("@Id", mark.Id),
                            new SqliteParameter("@DisciplineId", mark.Discipline.Id),
                            new SqliteParameter("@WeekId", week.Id),
                            new SqliteParameter("@EntityNumber", mark.EntityNumber),
                        ];
                        await _context.Database.ExecuteSqlRawAsync(
                            assignmentSQL,
                            markParameters,
                            ct
                        );
                        currentMarkNumber++;
                        foreach (var studentAssignment in mark.StudentAssignments)
                        {
                            studentAssignment.SetNumber(currentStudentAssignmentNumber);
                            SqliteParameter[] studentAssignmentParameters =
                            [
                                new SqliteParameter("@Id", studentAssignment.Id),
                                new SqliteParameter(
                                    "@AssignmentId",
                                    studentAssignment.Assignment.Id
                                ),
                                new SqliteParameter("@StudentId", studentAssignment.Student.Id),
                                new SqliteParameter("@Value_Value", studentAssignment.Value.Value),
                                new SqliteParameter(
                                    "@EntityNumber",
                                    studentAssignment.EntityNumber
                                ),
                            ];
                            await _context.Database.ExecuteSqlRawAsync(
                                studentAssignmentSQL,
                                studentAssignmentParameters,
                                ct
                            );
                        }
                    }
                }

                await transaction.CommitAsync(ct);
            }
            catch
            {
                await transaction.RollbackAsync(ct);
            }
        }
    }

    public async Task<AssignmentSession?> GetById(Guid id, CancellationToken ct = default) =>
        await _context
            .Sessions.Include(s => s.Weeks)
            .ThenInclude(w => w.Assignments)
            .ThenInclude(a => a.StudentAssignments)
            .ThenInclude(sa => sa.Student)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Assignments)
            .ThenInclude(a => a.Discipline)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.ActiveGroupSemester)
            .ThenInclude(s => s!.Plan)
            .ThenInclude(p => p.Direction)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.EducationPlan)
            .ThenInclude(p => p!.Semesters)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken: ct);

    public async Task<int> Count(CancellationToken ct = default) =>
        await _context.Sessions.CountAsync(cancellationToken: ct);

    public async Task Remove(AssignmentSession session, CancellationToken ct = default) =>
        await _context
            .Sessions.Where(s => s.Id == session.Id)
            .ExecuteDeleteAsync(cancellationToken: ct);

    public async Task<int> GenerateEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Sessions.Select(s => s.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    public async Task<TeacherAssignmentSession?> GetAssignmentSessionForTeacher(
        Teacher teacher,
        CancellationToken ct = default
    )
    {
        var session = await _context
            .Sessions.Include(s => s.Weeks)
            .ThenInclude(w => w.Assignments)
            .ThenInclude(a => a.StudentAssignments)
            .ThenInclude(sa => sa.Student)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Assignments)
            .ThenInclude(a => a.Discipline)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.ActiveGroupSemester)
            .ThenInclude(s => s!.Plan)
            .ThenInclude(p => p.Direction)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.EducationPlan)
            .ThenInclude(p => p!.Semesters)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(
                a => a.State.State == AssignmentSessionState.Opened.State,
                cancellationToken: ct
            );
        return session == null ? null : new TeacherAssignmentSession(teacher, session);
    }

    public async Task<AssignmentSession?> GetActiveSession(CancellationToken ct = default) =>
        await _context
            .Sessions.Include(s => s.Weeks)
            .ThenInclude(w => w.Assignments)
            .ThenInclude(a => a.StudentAssignments)
            .ThenInclude(sa => sa.Student)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Assignments)
            .ThenInclude(a => a.Discipline)
            .ThenInclude(d => d.Teacher)
            .ThenInclude(t => t!.Department)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.ActiveGroupSemester)
            .ThenInclude(s => s!.Plan)
            .ThenInclude(p => p.Direction)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.EducationPlan)
            .ThenInclude(p => p!.Direction)
            .Include(s => s.Weeks)
            .ThenInclude(w => w.Group)
            .ThenInclude(g => g.EducationPlan)
            .ThenInclude(p => p!.Semesters)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(
                s => s.State.State == AssignmentSessionState.Opened.State,
                cancellationToken: ct
            );

    private async Task<int> GenerateWeekEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Weeks.Select(s => s.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    private async Task<int> GenerateAssignmentEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .Assignments.Select(a => a.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }

    private async Task<int> GenerateStudentAssignmentEntityNumber(CancellationToken ct = default)
    {
        var numbers = await _context
            .StudentAssignments.Select(a => a.EntityNumber)
            .ToArrayAsync(cancellationToken: ct);
        return numbers.GetOrderedValue();
    }
}
