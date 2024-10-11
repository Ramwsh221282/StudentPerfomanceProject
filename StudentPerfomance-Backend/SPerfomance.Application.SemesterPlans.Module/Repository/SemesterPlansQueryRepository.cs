using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;

namespace SPerfomance.Application.SemesterPlans.Module.Repository;

internal sealed class SemesterPlansQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() => await _context.SemesterPlans.CountAsync();
	public async Task Commit() => await _context.SaveChangesAsync();
	public async Task<bool> HasEqualRecord(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<SemesterPlan>> GetAll() =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.Include(p => p.AttachedTeacher)
		.OrderByDescending(p => p.EntityNumber)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<SemesterPlan>> GetPaged(int page, int pageSize) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.Include(p => p.AttachedTeacher)
		.OrderByDescending(p => p.EntityNumber)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<SemesterPlan?> GetByParameter(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.Include(p => p.AttachedTeacher)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<Teacher?> GetByParameter(IRepositoryExpression<Teacher> expression) =>
		await _context.Teachers
		.Include(t => t.Department)
		.Include(t => t.Disciplines)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<SemesterPlan>> GetFiltered(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.Include(p => p.AttachedTeacher)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<SemesterPlan>> GetFilteredAndPaged(IRepositoryExpression<SemesterPlan> expression, int page, int pageSize) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.Include(p => p.AttachedTeacher)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
}
