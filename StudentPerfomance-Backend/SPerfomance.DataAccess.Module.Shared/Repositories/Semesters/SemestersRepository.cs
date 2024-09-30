using Microsoft.EntityFrameworkCore;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;

internal sealed class SemestersRepository : IRepository<Semester>
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() => await _context.Semesters.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Semester> expression) =>
		await _context.Semesters
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<Semester>> GetAll() =>
		await _context.Semesters
		.Include(s => s.Contracts)
		.ThenInclude(c => c.LinkedSemester)
		.OrderByDescending(s => s.Number.Value)
		.AsNoTracking()
		.ToListAsync();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task Remove(Semester entity)
	{
		await _context.SemesterPlans
		.Where(p => p.LinkedSemester.Id == entity.Id)
		.ExecuteDeleteAsync();
		await _context.Semesters
		.Where(s => s.Id == entity.Id)
		.ExecuteDeleteAsync();
		await Commit();
	}

	public async Task Create(Semester entity)
	{
		entity.SetNumber(await GenerateEntityNumber());
		Console.WriteLine(entity.Number);
		await _context.Semesters.AddAsync(entity);
		await Commit();
	}

	public async Task<IReadOnlyCollection<Semester>> GetPaged(int page, int pageSize) =>
		await _context.Semesters
		.Include(s => s.Contracts)
		.OrderByDescending(s => s.Number.Value)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Semester?> GetByParameter(IRepositoryExpression<Semester> expression) =>
		await _context.Semesters
		.Include(s => s.Contracts)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<Semester>> GetFiltered(IRepositoryExpression<Semester> expression) =>
		await _context.Semesters
		.Include(s => s.Contracts)
		.OrderByDescending(s => s.Number.Value)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<Semester>> GetFilteredAndPaged(IRepositoryExpression<Semester> expression, int page, int pageSize) =>
		await _context.Semesters
		.Include(s => s.Contracts)
		.OrderByDescending(s => s.Number.Value)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<int> CountWithExpression(IRepositoryExpression<Semester> expression) =>
		await _context.Semesters
		.CountAsync(expression.Build());

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count + 1;
	}
}
