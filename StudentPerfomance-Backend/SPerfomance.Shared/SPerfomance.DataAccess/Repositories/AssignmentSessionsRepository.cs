using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SPerfomance.Domain.Models.PerfomanceContext.Models.Assignments;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSessions.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentsWeeks;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class AssignmentSessionsRepository : IAssignmentSessionsRepository
{
	private readonly DatabaseContext _context = new DatabaseContext();

	public async Task Insert(AssignmentSession session)
	{
		using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
		{
			try
			{
				int currentWeekNumbers = await GenerateWeekEntityNumber();
				int currentMarkNumber = await GenerateAssignmentEntityNumber();

				session.SetNumber(await GenerateEntityNumber());
				string sessionSQL =
		"""
		INSERT INTO Sessions(Id, SessionStartDate, SessionCloseDate, State_State, EntityNumber)
		VALUES
		(@Id, @StartDate, @EndDate, @State, @EntityNumber)
		""";
				SqliteParameter[] sessionParameters = [
					new SqliteParameter("@Id", session.Id),
			new SqliteParameter("@StartDate", session.SessionStartDate),
			new SqliteParameter("@EndDate", session.SessionCloseDate),
			new SqliteParameter("@State", session.State.State),
			new SqliteParameter("@EntityNumber", session.EntityNumber),
		];
				await _context.Database.ExecuteSqlRawAsync(sessionSQL, sessionParameters);

				string weekSQL =
				"""
		INSERT INTO Weeks(Id, SessionId, GroupId, EntityNumber)
		VALUES
		(@Id, @SessionId, @GroupId, @EntityNumber)
		""";

				string assignmentSQL =
				"""
		INSERT INTO Assignments (Id, WeekId, AssignmentOpenDate, Assigner_Name, Assigner_Surname, Assigner_Patronymic, AssignerDepartment_Name, AssignedTo_Name, AssignedTo_Patronymic, AssignedTo_Surname, AssignedToRecordBook_Recordbook, AssignetToGroup_Name, Discipline_Name, State_State, EntityNumber)
		VALUES (@Id, @WeekId, @OpenDate, @AssignerName, @AssignerSurname, @AssignerPatronymic, @AssignerDepartmentName, @Name, @Patronymic, @Surname, @Recordbook, @Group, @Discipline, @State, @EntityNumber)
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
					await _context.Database.ExecuteSqlRawAsync(weekSQL, weekParameters);
					currentWeekNumbers++;
					foreach (Assignment mark in week.Assignments)
					{
						mark.SetNumber(currentMarkNumber);
						SqliteParameter[] markParameters = [
							new SqliteParameter("@Id", mark.Id),
							new SqliteParameter("@WeekId", week.Id),
							new SqliteParameter("@OpenDate", week.Session.SessionStartDate),
							new SqliteParameter("@AssignerName", mark.Assigner.Name),
							new SqliteParameter("@AssignerSurname", mark.Assigner.Surname),
							new SqliteParameter("@AssignerPatronymic", mark.Assigner.Patronymic),
							new SqliteParameter("@AssignerDepartmentName", mark.AssignerDepartment.Name),
							new SqliteParameter("@Name", mark.AssignedTo.Name),
							new SqliteParameter("@Surname", mark.AssignedTo.Surname),
							new SqliteParameter("@Patronymic", mark.AssignedTo.Patronymic),
							new SqliteParameter("@Recordbook", mark.AssignedToRecordBook.Recordbook),
							new SqliteParameter("@Group", mark.AssignetToGroup.Name),
							new SqliteParameter("@Discipline", mark.Discipline.Name),
							new SqliteParameter("@State", mark.State.State),
							new SqliteParameter("@EntityNumber", mark.EntityNumber)
						];
						await _context.Database.ExecuteSqlRawAsync(assignmentSQL, markParameters);
						currentMarkNumber++;
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

	public async Task<int> Count() => await _context.Sessions.CountAsync();

	public async Task Remove(AssignmentSession session) =>
		await _context.Sessions.Where(s => s.Id == session.Id)
		.ExecuteDeleteAsync();

	public async Task Update(AssignmentSession session) =>
		await _context.Sessions.Where(s => s.Id == session.Id)
		.ExecuteUpdateAsync(s =>
			s.SetProperty(s => s.SessionCloseDate, session.SessionCloseDate)
			.SetProperty(s => s.State.State, session.State.State)
		);

	public async Task<IReadOnlyCollection<AssignmentSession>> GetPaged(int page, int pageSize) =>
		await _context.Sessions
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.OrderBy(s => s.EntityNumber)
		.Include(s => s.Weeks)
		.ThenInclude(w => w.Assignments)
		.Include(s => s.Weeks)
		.ThenInclude(w => w.Group)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<IReadOnlyCollection<AssignmentSession>> GetInPeriodPaged(
		DateTime startDate,
		DateTime endDate,
		int page,
		int pageSize
		) =>
	await _context.Sessions
		.Where(s =>
		s.SessionStartDate.Day == startDate.Day &&
		s.SessionStartDate.Month == startDate.Month &&
		s.SessionStartDate.Year == startDate.Year &&
		s.SessionCloseDate.Day == endDate.Day &&
		s.SessionCloseDate.Month == endDate.Month &&
		s.SessionCloseDate.Year == endDate.Year)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.OrderBy(s => s.EntityNumber)
		.Include(s => s.Weeks)
		.ThenInclude(w => w.Assignments)
		.Include(s => s.Weeks)
		.ThenInclude(w => w.Group)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task GetByPeriod(DateTime startDate, DateTime endDate) =>
		await _context.Sessions
		.Where(
			s =>
			s.SessionCloseDate.Day == endDate.Day &&
			s.SessionCloseDate.Month == endDate.Month &&
			s.SessionCloseDate.Year == endDate.Year &&
			s.SessionStartDate.Day == startDate.Day &&
			s.SessionStartDate.Month == startDate.Month &&
			s.SessionStartDate.Year == startDate.Year
		)
		.Include(s => s.Weeks)
		.ThenInclude(w => w.Assignments)
		.Include(s => s.Weeks)
		.ThenInclude(w => w.Group)
		.AsNoTracking()
		.AsSplitQuery()
		.ToListAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Sessions.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}

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
