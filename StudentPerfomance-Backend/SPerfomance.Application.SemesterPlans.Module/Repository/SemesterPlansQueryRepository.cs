using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
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
		.ThenInclude(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(p => p.AttachedTeacher)
		.ThenInclude(t => t.Department)
		.OrderByDescending(p => p.EntityNumber)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<SemesterPlan>> GetPaged(int page, int pageSize) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.ThenInclude(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(p => p.AttachedTeacher)
		.ThenInclude(t => t.Department)
		.OrderByDescending(p => p.EntityNumber)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Semester?> GetByParameter(IRepositoryExpression<Semester> expression) =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.ThenInclude(t => t.Department)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<SemesterPlan?> GetByParameter(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.ThenInclude(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(p => p.AttachedTeacher)
		.ThenInclude(t => t.Department)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<Teacher?> GetByParameter(IRepositoryExpression<Teacher> expression) =>
		await _context.Teachers
		.Include(t => t.Department)
		.Include(t => t.Disciplines)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<SemesterPlan>> GetFiltered(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.ThenInclude(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(p => p.AttachedTeacher)
		.ThenInclude(t => t.Department)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<SemesterPlan>> GetFilteredAndPaged(IRepositoryExpression<SemesterPlan> expression, int page, int pageSize) =>
		await _context.SemesterPlans
		.Include(p => p.Semester)
		.ThenInclude(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(p => p.AttachedTeacher)
		.ThenInclude(t => t.Department)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
}
