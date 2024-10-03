using Microsoft.EntityFrameworkCore;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

namespace SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;

public sealed class EducationPlansRepository : IRepository<EducationPlan>, IFluentCreatableRepository<EducationPlan>
{
	private readonly ApplicationDb _db = new ApplicationDb();
	public async Task Commit() => await _db.SaveChangesAsync();
	public async Task<int> Count() => await _db.EducationPlans.CountAsync();
	public async Task<int> CountWithExpression(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans.CountAsync(expression.Build());
	public async Task Create(EducationPlan entity)
	{
		_db.EducationDirections.Attach(entity.Direction);
		entity.SetNumber(await GenerateEntityNumber());
		await _db.EducationPlans.AddAsync(entity);
		await Commit();
	}

	public async Task<IReadOnlyCollection<EducationPlan>> GetAll() =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.AsNoTracking()
		.ToListAsync();


	public async Task<EducationPlan?> GetByParameter(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<EducationPlan>> GetFiltered(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<EducationPlan>> GetFilteredAndPaged(IRepositoryExpression<EducationPlan> expression, int page, int pageSize) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.Where(expression.Build())
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<EducationPlan>> GetPaged(int page, int pageSize) =>
		await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Semesters)
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<EducationPlan> expression) =>
		await _db.EducationPlans.AnyAsync(expression.Build());

	public async Task Remove(EducationPlan entity)
	{
		SemestersRepository semesters = new SemestersRepository();
		foreach (var semester in entity.Semesters)
		{
			await semesters.Remove(semester);
		}
		_db.Attach(entity.Direction);
		await _db.EducationPlans.Where(p => p.Id == entity.Id).ExecuteDeleteAsync();
		await Commit();
	}

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count + 1;
	}

	public async Task<EducationPlan> FluentCreate(EducationPlan entity)
	{
		_db.Attach(entity.Direction);
		entity.SetNumber(await GenerateEntityNumber());
		await _db.EducationPlans.AddAsync(entity);
		return entity;
	}
}
