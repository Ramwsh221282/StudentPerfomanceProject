using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.TeachersDepartments;

public sealed class TeacherDepartmentsRepository : IRepository<TeachersDepartment>
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() => await _context.Departments.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<TeachersDepartment> expression) =>
		await _context.Departments
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetAll() =>
		await _context.Departments
		.Include(d => d.Teachers)
		.OrderByDescending(d => d.Name)
		.AsNoTracking()
		.ToListAsync();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task Remove(TeachersDepartment entity)
	{
		foreach (Teacher teacher in entity.Teachers)
		{
			await _context.Grades.Where(g => g.Teacher.Id == teacher.Id).ExecuteDeleteAsync();
			await _context.Disciplines.Where(d => d.Teacher.Id == teacher.Id).ExecuteDeleteAsync();
			await _context.Teachers.Where(t => t.Id == teacher.Id).ExecuteDeleteAsync();
		}
		await _context.Departments.Where(d => d.Id == entity.Id)
		.ExecuteDeleteAsync();
		await Commit();
	}

	public async Task Create(TeachersDepartment entity)
	{
		await _context.Departments.AddAsync(entity);
		await Commit();
	}

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetPaged(int page, int pageSize) =>
		await _context.Departments
		.Include(d => d.Teachers)
		.OrderByDescending(d => d.Name)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetFiltered(IRepositoryExpression<TeachersDepartment> expression)
	{
		var criteria = expression.Build();
		return await _context.Departments
		.Include(d => d.Teachers)
		.OrderByDescending(d => d.Name)
		.Where(criteria)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetFilteredAndPaged(IRepositoryExpression<TeachersDepartment> expression, int page, int pageSize)
	{
		var criteria = expression.Build();
		return await _context.Departments
		.Include(d => d.Teachers)
		.OrderByDescending(d => d.Name)
		.Where(criteria)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<TeachersDepartment?> GetByParameter(IRepositoryExpression<TeachersDepartment> expression)
	{
		var criteria = expression.Build();
		return await _context.Departments
		.Include(d => d.Teachers)
		.FirstOrDefaultAsync(criteria);
	}

	public async Task<int> CountWithExpression(IRepositoryExpression<TeachersDepartment> expression) =>
		await _context.Departments.CountAsync(expression.Build());

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count + 1;
	}
}
