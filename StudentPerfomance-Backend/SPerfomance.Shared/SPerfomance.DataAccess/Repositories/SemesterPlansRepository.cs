using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class SemesterPlansRepository : ISemesterPlansRepository
{
	private readonly DatabaseContext _context = new DatabaseContext();

	public async Task DeattachTeacherId(SemesterPlan entity) =>
		await _context.SemesterPlans
		.Where(s => s.Id == entity.Id)
		.ExecuteUpdateAsync(s =>
			s.SetProperty(s => s.Teacher, (Teacher?)null));

	public async Task AttachTeacherId(SemesterPlan entity) =>
		await _context.SemesterPlans
		.Where(s => s.Id == entity.Id)
		.ExecuteUpdateAsync(s =>
			s.SetProperty(s => s.Teacher, entity.Teacher));

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.SemesterPlans.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}

	public async Task Insert(SemesterPlan entity)
	{
		_context.Semesters.Attach(entity.Semester);
		entity.SetNumber(await GenerateEntityNumber());
		await _context.SemesterPlans.AddAsync(entity);
		await _context.SaveChangesAsync();
		// string sql =
		// "INSERT INTO SemesterPlans (Id, Discipline_Name, SemesterId, EntityNumber) VALUES (@Id, @Discipline_Name, @SemesterId, @EntityNumber);";
		// var parameters = new[]
		// {
		// 	new SqliteParameter("@Id", entity.Id),
		// 	new SqliteParameter("@Discipline_Name", entity.Discipline.Name),
		// 	new SqliteParameter("@SemesterId", entity.Semester.Id),
		// 	new SqliteParameter("@EntityNumber", entity.EntityNumber)
		// };
		// await _context.Database.ExecuteSqlRawAsync(sql, parameters);
	}

	public async Task Remove(SemesterPlan entity) =>
		await _context.SemesterPlans
		.Where(s => s.Id == entity.Id)
		.ExecuteDeleteAsync();

	public async Task Update(SemesterPlan entity) =>
		await _context.SemesterPlans
		.Where(s => s.Id == entity.Id)
		.ExecuteUpdateAsync(s =>
			s.SetProperty(s => s.Discipline.Name, entity.Discipline.Name));
}
