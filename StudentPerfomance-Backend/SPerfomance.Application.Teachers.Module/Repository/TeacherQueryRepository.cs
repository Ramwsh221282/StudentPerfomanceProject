using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.Teachers.Module.Repository;

internal sealed class TeacherQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task<IReadOnlyCollection<Teacher>> GetAll() =>
		await _context.Teachers
		.Include(t => t.Disciplines)
		.Include(t => t.Department)
		.OrderBy(t => t.EntityNumber)
		.ThenByDescending(t => t.Name.Surname)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<Teacher>> GetPaged(int page, int pageSize) =>
		await _context.Teachers
		.Include(t => t.Disciplines)
		.Include(t => t.Department)
		.OrderBy(t => t.EntityNumber)
		.ThenByDescending(t => t.Name.Surname)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Teacher?> GetByParameter(IRepositoryExpression<Teacher> expression)
	{
		var criteria = expression.Build();
		return await _context.Teachers
		.Include(t => t.Disciplines)
		.Include(t => t.Department)
		.FirstOrDefaultAsync(criteria);
	}

	public async Task<TeachersDepartment?> GetByParameter(IRepositoryExpression<TeachersDepartment> expression)
	{
		var criteria = expression.Build();
		return await _context.Departments
		.Include(d => d.Teachers)
		.ThenInclude(t => t.Disciplines)
		.FirstOrDefaultAsync(criteria);
	}

	public async Task<IReadOnlyCollection<Teacher>> GetFiltered(IRepositoryExpression<Teacher> expression)
	{
		var criteria = expression.Build();
		return await _context.Teachers
		.Include(t => t.Disciplines)
		.Include(t => t.Department)
		.Where(criteria)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<IReadOnlyCollection<Teacher>> GetFilteredAndPaged(IRepositoryExpression<Teacher> expression, int page, int pageSize)
	{
		var criteria = expression.Build();
		return await _context.Teachers
		.Include(t => t.Disciplines)
		.Include(t => t.Department)
		.Where(criteria)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Teacher> expression) =>
		await _context.Teachers
		.AnyAsync(expression.Build());

	public async Task<int> Count() => await _context.Teachers.CountAsync();

	public async Task Commit() => await _context.SaveChangesAsync();
}
