using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.ValueObject;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class AssignmentSessionsRepository : IAssignmentSessionsRepository
{
    private readonly DatabaseContext _context = new DatabaseContext();

    public async Task Insert(AssignmentSession session)
    {
        await using (
            IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync()
        )
        {
            try
            {
                int currentWeekNumbers = await GenerateWeekEntityNumber();
                int currentMarkNumber = await GenerateAssignmentEntityNumber();
                int currentStudentAssignmentNumber = await GenerateStudentAssignmentEntityNumber();

                session.SetNumber(await GenerateEntityNumber());
                string sessionSql = """
                    INSERT INTO Sessions(Id, SessionStartDate, SessionCloseDate, State_State, EntityNumber)
                    VALUES
                    (@Id, @StartDate, @EndDate, @State, @EntityNumber)
                    """;
                SqliteParameter[] sessionParameters =
                [
                    new SqliteParameter("@Id", session.Id),
                    new SqliteParameter("@StartDate", session.SessionStartDate),
                    new SqliteParameter("@EndDate", session.SessionCloseDate),
                    new SqliteParameter("@State", session.State.State),
                    new SqliteParameter("@EntityNumber", session.EntityNumber),
                ];
                await _context.Database.ExecuteSqlRawAsync(sessionSql, sessionParameters);

                string weekSql = """
                    INSERT INTO Weeks(Id, SessionId, GroupId, EntityNumber)
                    VALUES
                    (@Id, @SessionId, @GroupId, @EntityNumber)
                    """;

                string assignmentSQL = """
                    INSERT INTO Assignments (Id, DisciplineId, WeekId, EntityNumber)
                    VALUES (@Id, @DisciplineId, @WeekId, @EntityNumber)
                    """;

                string studentAssignmentSQL = """
                    INSERT INTO StudentAssignments (Id, AssignmentId, StudentId, Value_Value, EntityNumber)
                    VALUES (@Id, @AssignmentId, @StudentId, @Value_Value, @EntityNumber)
                    """;

                foreach (AssignmentWeek week in session.Weeks)
                {
                    week.SetNumber(currentWeekNumbers);
                    SqliteParameter[] weekParameters =
                    [
                        new SqliteParameter("@Id", week.Id),
                        new SqliteParameter("@SessionId", week.Session.Id),
                        new SqliteParameter("@GroupId", week.Group!.Id),
                        new SqliteParameter("@EntityNumber", week.EntityNumber),
                    ];
                    await _context.Database.ExecuteSqlRawAsync(weekSql, weekParameters);
                    currentWeekNumbers++;
                    foreach (Assignment mark in week.Assignments)
                    {
                        mark.SetNumber(currentMarkNumber);
                        SqliteParameter[] markParameters =
                        [
                            new SqliteParameter("@Id", mark.Id),
                            new SqliteParameter("@DisciplineId", mark.Discipline.Id),
                            new SqliteParameter("@WeekId", week.Id),
                            new SqliteParameter("@EntityNumber", mark.EntityNumber),
                        ];
                        await _context.Database.ExecuteSqlRawAsync(assignmentSQL, markParameters);
                        currentMarkNumber++;
                        foreach (StudentAssignment studentAssignment in mark.StudentAssignments)
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
                                studentAssignmentParameters
                            );
                        }
                    }
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }
        }
    }

    public async Task<AssignmentSession?> GetById(Guid id) =>
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
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task<int> Count() => await _context.Sessions.CountAsync();

    public async Task Remove(AssignmentSession session) =>
        await _context.Sessions.Where(s => s.Id == session.Id).ExecuteDeleteAsync();

    public async Task Update(AssignmentSession session) =>
        await _context
            .Sessions.Where(s => s.Id == session.Id)
            .ExecuteUpdateAsync(s =>
                s.SetProperty(s => s.SessionCloseDate, session.SessionCloseDate)
                    .SetProperty(s => s.State.State, session.State.State)
            );

    public async Task<IReadOnlyCollection<AssignmentSession>> GetPaged(int page, int pageSize) =>
        await _context
            .Sessions.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(s => s.EntityNumber)
            .Include(s => s.Weeks)
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
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task<IReadOnlyCollection<AssignmentSession>> GetInPeriodPaged(
        DateTime startDate,
        DateTime endDate,
        int page,
        int pageSize
    ) =>
        await _context
            .Sessions.Where(s =>
                s.SessionStartDate.Day == startDate.Day
                && s.SessionStartDate.Month == startDate.Month
                && s.SessionStartDate.Year == startDate.Year
                && s.SessionCloseDate.Day == endDate.Day
                && s.SessionCloseDate.Month == endDate.Month
                && s.SessionCloseDate.Year == endDate.Year
            )
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(s => s.EntityNumber)
            .Include(s => s.Weeks)
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
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task GetByPeriod(DateTime startDate, DateTime endDate) =>
        await _context
            .Sessions.Where(s =>
                s.SessionCloseDate.Day == endDate.Day
                && s.SessionCloseDate.Month == endDate.Month
                && s.SessionCloseDate.Year == endDate.Year
                && s.SessionStartDate.Day == startDate.Day
                && s.SessionStartDate.Month == startDate.Month
                && s.SessionStartDate.Year == startDate.Year
            )
            .Include(s => s.Weeks)
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
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync();

    public async Task<int> GenerateEntityNumber()
    {
        int[] numbers = await _context.Sessions.Select(s => s.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public async Task<TeacherAssignmentSession?> GetAssignmentSessionForTeacher(Teacher teacher)
    {
        AssignmentSession? session = await _context
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
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(a => a.State.State == AssignmentSessionState.Opened.State);
        return session == null ? null : new TeacherAssignmentSession(teacher, session);
    }

    public async Task<AssignmentSession?> GetActiveSession() =>
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
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(s => s.State.State == AssignmentSessionState.Opened.State);

    private async Task<int> GenerateWeekEntityNumber()
    {
        int[] numbers = await _context.Weeks.Select(s => s.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    private async Task<int> GenerateAssignmentEntityNumber()
    {
        int[] numbers = await _context.Assignments.Select(a => a.EntityNumber).ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    private async Task<int> GenerateStudentAssignmentEntityNumber()
    {
        int[] numbers = await _context
            .StudentAssignments.Select(a => a.EntityNumber)
            .ToArrayAsync();
        return numbers.GetOrderedValue();
    }

    public void DoBackgroundWork()
    {
        DateTime current = DateTime.Now;
        Console.WriteLine("Current date:");
        Console.WriteLine(current.ToString());
        Console.WriteLine("Making a query to check session with open date");
        bool hasSession = _context.Sessions.Any(s => s.SessionStartDate >= current);
        Console.WriteLine(hasSession);
    }
}
