using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.TeacherDepartments.Module.Repository;

internal sealed class TeacherDepartmentsQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task<int> Count() => await _context.Departments.CountAsync();

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetAll() =>
		await _context.Departments
		.Include(d => d.Teachers)
		.OrderBy(d => d.EntityNumber)
		.ThenByDescending(d => d.FullName)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetPaged(int page, int pageSize) =>
		await _context.Departments
		.Include(d => d.Teachers)
		.OrderBy(d => d.EntityNumber)
		.ThenByDescending(d => d.FullName)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetFiltered(IRepositoryExpression<TeachersDepartment> expression)
	{
		var criteria = expression.Build();
		return await _context.Departments
		.Include(d => d.Teachers)
		.OrderByDescending(d => d.FullName)
		.Where(criteria)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<IReadOnlyCollection<TeachersDepartment>> GetFilteredAndPaged(IRepositoryExpression<TeachersDepartment> expression, int page, int pageSize)
	{
		var criteria = expression.Build();
		return await _context.Departments
		.Include(d => d.Teachers)
		.OrderByDescending(d => d.FullName)
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

	public async Task<bool> HasEqualRecord(IRepositoryExpression<TeachersDepartment> expression) =>
		await _context.Departments
		.AnyAsync(expression.Build());

	public async Task Commit() => await _context.SaveChangesAsync();
}
