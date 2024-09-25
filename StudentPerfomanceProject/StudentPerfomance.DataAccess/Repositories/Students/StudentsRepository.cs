using Microsoft.EntityFrameworkCore;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.Students;

public sealed class StudentsRepository : IRepository<Student>
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() =>
		await _context.Students.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Student> expression) =>
		await _context.Students
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<Student>> GetAll() =>
		await _context.Students
		.Include(s => s.Group)
		.ThenInclude(g => g.Students)
		.OrderByDescending(s => s.Name.Surname)
		.AsNoTracking()
		.ToListAsync();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task Remove(Student entity)
	{
		_context.Groups.Attach(entity.Group);
		await _context.Students.
		Where(s => s.Id == entity.Id)
		.ExecuteDeleteAsync();
		await Commit();
	}

	public async Task Create(Student entity)
	{
		_context.Groups.Attach(entity.Group);
		await _context.AddAsync(entity);
		await Commit();
	}

	public async Task<IReadOnlyCollection<Student>> GetPaged(int page, int pageSize) =>
		await _context.Students
		.Include(s => s.Group)
		.OrderByDescending(s => s.Name.Surname)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Student?> GetByParameter(IRepositoryExpression<Student> expression)
	{
		var criteria = expression.Build();
		return await _context.Students
		.Include(s => s.Group)
		.ThenInclude(g => g.Students)
		.OrderByDescending(s => s.Name.Surname)
		.FirstOrDefaultAsync(criteria);
	}

	public async Task<IReadOnlyCollection<Student>> GetFiltered(IRepositoryExpression<Student> expression)
	{
		var criteria = expression.Build();
		return await _context.Students
		.Include(s => s.Group)
		.OrderByDescending(s => s.Name.Surname)
		.Where(criteria)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<IReadOnlyCollection<Student>> GetFilteredAndPaged(IRepositoryExpression<Student> expression, int page, int pageSize)
	{
		var criteria = expression.Build();
		return await _context.Students
		.Include(s => s.Group)
		.OrderByDescending(s => s.Name.Surname)
		.Where(criteria)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<int> CountWithExpression(IRepositoryExpression<Student> expression) =>
		await _context.Students.CountAsync(expression.Build());

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count + 1;
	}
}
