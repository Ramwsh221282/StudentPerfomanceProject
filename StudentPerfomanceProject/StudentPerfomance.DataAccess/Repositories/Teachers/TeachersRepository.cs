using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Teachers;

public sealed class TeachersRepository : IRepository<Teacher>
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() => await _context.Teachers.CountAsync();
	public async Task<bool> HasEqualRecord(IRepositoryExpression<Teacher> expression) =>
		await _context.Teachers
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<Teacher>> GetAll() =>
		await _context.Teachers
		.Include(t => t.Department)
		.ThenInclude(d => d.Teachers)
		.OrderByDescending(t => t.Name.Surname)
		.AsNoTracking()
		.ToListAsync();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task Remove(Teacher entity)
	{
		await _context.Grades.Where(g => g.Teacher.Id == entity.Id).ExecuteDeleteAsync();
		await _context.Teachers.Where(t => t.Id == entity.Id).ExecuteDeleteAsync();
		await Commit();
	}

	public async Task Create(Teacher entity)
	{
		_context.Departments.Attach(entity.Department);
		await _context.Teachers.AddAsync(entity);
		await _context.SaveChangesAsync();
		await Commit();
	}

	public async Task<IReadOnlyCollection<Teacher>> GetPaged(int page, int pageSize) =>
		await _context.Teachers
		.Include(t => t.Department)
		.OrderByDescending(t => t.Name.Surname)
		.Skip((1 - page) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Teacher?> GetByParameter(IRepositoryExpression<Teacher> expression)
	{
		var criteria = expression.Build();
		return await _context.Teachers
		.Include(t => t.Department)
		.FirstOrDefaultAsync(criteria);
	}

	public async Task<IReadOnlyCollection<Teacher>> GetFiltered(IRepositoryExpression<Teacher> expression)
	{
		var criteria = expression.Build();
		return await _context.Teachers
		.Include(t => t.Department)
		.Where(criteria)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<IReadOnlyCollection<Teacher>> GetFilteredAndPaged(IRepositoryExpression<Teacher> expression, int page, int pageSize)
	{
		var criteria = expression.Build();
		return await _context.Teachers
		.Include(t => t.Department)
		.Where(criteria)
		.Skip((1 - page) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<int> CountWithExpression(IRepositoryExpression<Teacher> expression) =>
		await _context.Teachers.CountAsync(expression.Build());

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count == 0 ? 1 : count++;
	}
}
