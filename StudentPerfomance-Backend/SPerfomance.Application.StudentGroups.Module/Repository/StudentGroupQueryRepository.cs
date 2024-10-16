using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Repository;

internal sealed class StudentGroupQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> GetAll() =>
		await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderByDescending(g => g.Name.Name)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> GetPaged(int page, int pageSize)
	{
		List<StudentGroup> groups = await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderBy(g => g.EntityNumber)
		.ThenByDescending(g => g.Name.Name)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
		return groups;
	}

	public async Task<StudentGroup?> GetByParameter(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<EducationPlan?> GetByParameter(IRepositoryExpression<EducationPlan> expression) =>
		await _context.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Groups)
		.ThenInclude(g => g.Students)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<StudentGroup>> GetFiltered(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderBy(g => g.EntityNumber)
		.ThenByDescending(g => g.Name.Name)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<StudentGroup>> GetFilteredAndPaged(IRepositoryExpression<StudentGroup> expression, int page, int pageSize)
	{
		List<StudentGroup> groups = await _context.Groups
		.Include(g => g.Students)
		.Include(g => g.EducationPlan)
		.ThenInclude(p => p.Direction)
		.OrderByDescending(g => g.Name.Name)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
		return groups;
	}

	public async Task<int> Count() => await _context.Groups.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<StudentGroup> expression) =>
		await _context.Groups
		.AnyAsync(expression.Build());
}
