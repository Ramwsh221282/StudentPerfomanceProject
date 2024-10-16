using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;

namespace SPerfomance.Application.Semester.Module.Repository;

internal sealed class SemesterQueryRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> GetPaged(int page, int pageSize) =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.OrderBy(s => s.Number.Value)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<Domain.Module.Shared.Entities.Semesters.Semester?> GetByParameter(IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> expression) =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> GetFiltered(IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> expression) =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.OrderBy(s => s.Number.Value)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> GetFilteredAndPaged(IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> expression, int page, int pageSize) =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.OrderBy(s => s.Number.Value)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<int> Count() => await _context.Semesters.CountAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Domain.Module.Shared.Entities.Semesters.Semester> expression) =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<Domain.Module.Shared.Entities.Semesters.Semester>> GetAll() =>
		await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.OrderBy(s => s.Number.Value)
		.AsNoTracking()
		.ToListAsync();
}
