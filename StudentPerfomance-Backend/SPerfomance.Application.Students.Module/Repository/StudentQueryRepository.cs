using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.Students;

namespace SPerfomance.Application.Students.Module.Repository;

internal sealed class StudentQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<IReadOnlyCollection<Student>> GetPaged(int page, int pageSize) =>
		await _context.Students
		.Include(s => s.Group)
		.OrderBy(s => s.EntityNumber)
		.ThenByDescending(s => s.Name.Surname)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Student?> GetByParameter(IRepositoryExpression<Student> expression)
	{
		var criteria = expression.Build();
		return await _context.Students
		.Include(s => s.Group)
		.ThenInclude(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderByDescending(s => s.Name.Surname)
		.FirstOrDefaultAsync(criteria);
	}

	public async Task<IReadOnlyCollection<Student>> GetFiltered(IRepositoryExpression<Student> expression)
	{
		var criteria = expression.Build();
		return await _context.Students
		.Include(s => s.Group)
		.ThenInclude(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
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
		.ThenInclude(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderBy(s => s.EntityNumber)
		.ThenByDescending(s => s.Name.Surname)
		.Where(criteria)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	}

	public async Task<int> Count() =>
		await _context.Students.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Student> expression) =>
		await _context.Students
		.AnyAsync(expression.Build());

	public async Task<StudentGroup?> GetByParameter(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<Student>> GetAll() =>
		await _context.Students
		.Include(s => s.Group)
		.ThenInclude(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderBy(s => s.EntityNumber)
		.ThenByDescending(s => s.Name.Surname)
		.AsNoTracking()
		.ToListAsync();

	public async Task Commit() => await _context.SaveChangesAsync();
}
