using Microsoft.EntityFrameworkCore;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Disciplines;

public sealed class DisciplineRepository : IRepository<Discipline>
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<int> Count() => await _context.Disciplines.CountAsync();

	public async Task<int> CountWithExpression(IRepositoryExpression<Discipline> expression) =>
		await _context.Disciplines.CountAsync(expression.Build());

	public async Task Create(Discipline entity)
	{
		await _context.Disciplines.AddAsync(entity);
		await Commit();
	}

	public async Task<IReadOnlyCollection<Discipline>> GetAll() =>
		await _context.Disciplines
		.Include(d => d.Teacher)
		.ThenInclude(t => t.Disciplines)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Discipline?> GetByParameter(IRepositoryExpression<Discipline> expression) =>
		await _context.Disciplines
		.Include(d => d.Teacher)
		.ThenInclude(t => t.Disciplines)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<Discipline>> GetFiltered(IRepositoryExpression<Discipline> expression) =>
		await _context.Disciplines
		.Include(d => d.Teacher)
		.ThenInclude(t => t.Disciplines)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<Discipline>> GetFilteredAndPaged(IRepositoryExpression<Discipline> expression, int page, int pageSize) =>
		await _context.Disciplines
		.Include(d => d.Teacher)
		.ThenInclude(t => t.Disciplines)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<Discipline>> GetPaged(int page, int pageSize) =>
		await _context.Disciplines
		.Include(d => d.Teacher)
		.ThenInclude(t => t.Disciplines)
		.Skip((1 - page) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Discipline> expression) =>
		await _context.Disciplines
		.AnyAsync(expression.Build());

	public async Task Remove(Discipline entity)
	{
		await _context.Grades
		.Where(g => g.Discipline.Id == entity.Id)
		.ExecuteDeleteAsync();
		await _context.Disciplines.
		Where(d => d.Id == entity.Id)
		.ExecuteDeleteAsync();
		await Commit();
	}

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count == 0 ? 1 : count++;
	}
}
