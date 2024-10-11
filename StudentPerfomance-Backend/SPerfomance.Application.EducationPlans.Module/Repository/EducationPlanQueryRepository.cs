using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.Application.EducationPlans.Module.Repository;

internal sealed class EducationPlanQueryRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();
	public async Task Commit() => await _db.SaveChangesAsync();
	public async Task<int> Count() => await _db.EducationPlans.CountAsync();

	public async Task<IReadOnlyCollection<EducationPlan>> GetAll() =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.ThenInclude(s => s.Contracts)
		.AsNoTracking()
		.ToListAsync();

	public async Task<EducationPlan?> GetByParameter(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.ThenInclude(s => s.Contracts)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<EducationPlan>> GetFiltered(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.ThenInclude(s => s.Contracts)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<EducationPlan>> GetFilteredAndPaged(IRepositoryExpression<EducationPlan> expression, int page, int pageSize) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.ThenInclude(s => s.Contracts)
		.OrderBy(p => p.EntityNumber)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<EducationPlan>> GetPaged(int page, int pageSize) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.ThenInclude(s => s.Contracts)
		.OrderBy(p => p.EntityNumber)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans.AnyAsync(expression.Build());
}
