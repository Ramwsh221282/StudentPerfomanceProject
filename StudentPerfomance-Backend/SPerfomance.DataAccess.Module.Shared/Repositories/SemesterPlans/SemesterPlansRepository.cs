using Microsoft.EntityFrameworkCore;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;

public sealed class SemesterPlansRepository : IRepository<SemesterPlan>
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task<int> Count() => await _context.SemesterPlans.CountAsync();
	public async Task<bool> HasEqualRecord(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.AnyAsync(expression.Build());

	public async Task<IReadOnlyCollection<SemesterPlan>> GetAll() =>
		await _context.SemesterPlans
		.Include(p => p.LinkedSemester)
		.ThenInclude(s => s.Contracts)
		.Include(p => p.LinkedDiscipline)
		.OrderByDescending(p => p.LinkedDiscipline.Name)
		.AsNoTracking()
		.ToListAsync();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task Remove(SemesterPlan entity)
	{
		await _context.Grades.Where(g => g.Discipline.Id == entity.LinkedDiscipline.Id)
		.ExecuteDeleteAsync();
		await _context.Disciplines.Where(d => d.Id == entity.LinkedDiscipline.Id)
		.ExecuteDeleteAsync();
		await _context.SemesterPlans.Where(p => p.Id == entity.Id)
		.ExecuteDeleteAsync();
		await Commit();
	}

	public async Task Create(SemesterPlan entity)
	{
		_context.Semesters.Attach(entity.LinkedSemester);
		if (entity.LinkedDiscipline != null)
			_context.Attach(entity.LinkedDiscipline);
		await _context.SemesterPlans.AddAsync(entity);
		await Commit();
	}

	public async Task<IReadOnlyCollection<SemesterPlan>> GetPaged(int page, int pageSize) =>
		await _context.SemesterPlans
		.Include(p => p.LinkedSemester)
		.Include(p => p.LinkedDiscipline)
		.ThenInclude(d => d.Teacher)
		//.ThenInclude(t => t.Department)
		.OrderByDescending(p => p.LinkedDiscipline.Name)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<SemesterPlan?> GetByParameter(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.Include(p => p.LinkedSemester)
		.Include(p => p.LinkedDiscipline)
		.ThenInclude(d => d.Teacher)
		//.ThenInclude(t => t.Department)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<SemesterPlan>> GetFiltered(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.Include(p => p.LinkedSemester)
		.Include(p => p.LinkedDiscipline)
		.ThenInclude(d => d.Teacher)
		//.ThenInclude(t => t.Department)
		.OrderByDescending(p => p.LinkedDiscipline.Name)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<SemesterPlan>> GetFilteredAndPaged(IRepositoryExpression<SemesterPlan> expression, int page, int pageSize) =>
		await _context.SemesterPlans
		.Include(p => p.LinkedSemester)
		.Include(p => p.LinkedDiscipline)
		.ThenInclude(d => d.Teacher)
		//.ThenInclude(t => t.Department)
		.OrderByDescending(p => p.LinkedDiscipline.Name)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<int> CountWithExpression(IRepositoryExpression<SemesterPlan> expression) =>
		await _context.SemesterPlans
		.CountAsync(expression.Build());

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count + 1;
	}
}
