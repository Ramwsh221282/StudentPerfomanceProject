using Microsoft.EntityFrameworkCore;

using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.DataAccess.Repositories.EducationDirections;

public sealed class EducationDirectionRepository : IRepository<EducationDirection>
{
	private readonly ApplicationDb _db = new ApplicationDb();
	public async Task Commit() => await _db.SaveChangesAsync();
	public async Task<int> Count() => await _db.EducationDirections.CountAsync();
	public async Task<int> CountWithExpression(IRepositoryExpression<EducationDirection> expression) =>
		await _db.EducationDirections.CountAsync(expression.Build());
	public async Task Create(EducationDirection entity)
	{
		entity.SetNumber(await GenerateEntityNumber());
		await _db.EducationDirections.AddAsync(entity);
		await Commit();
	}
	public async Task<IReadOnlyCollection<EducationDirection>> GetAll() =>
		await _db.EducationDirections
		.AsNoTracking()
		.ToListAsync();
	public async Task<EducationDirection?> GetByParameter(IRepositoryExpression<EducationDirection> expression) =>
		await _db.EducationDirections.FirstOrDefaultAsync(expression.Build());
	public async Task<IReadOnlyCollection<EducationDirection>> GetFiltered(IRepositoryExpression<EducationDirection> expression) =>
		await _db.EducationDirections
		.Where(expression.Build())
		.AsNoTracking()
		.ToListAsync();
	public async Task<IReadOnlyCollection<EducationDirection>> GetFilteredAndPaged(IRepositoryExpression<EducationDirection> expression, int page, int pageSize) =>
		await _db.EducationDirections
		.Where(expression.Build())
		.Skip((1 - page) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	public async Task<IReadOnlyCollection<EducationDirection>> GetPaged(int page, int pageSize) =>
		await _db.EducationDirections
		.Skip((1 - page) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	public async Task<bool> HasEqualRecord(IRepositoryExpression<EducationDirection> expression) =>
		await _db.EducationDirections.AnyAsync(expression.Build());

	public async Task Remove(EducationDirection entity)
	{
		await _db.EducationDirections.Where(d => d.Id == entity.Id).ExecuteDeleteAsync();
		await Commit();
	}

	public async Task<int> GenerateEntityNumber()
	{
		int count = await Count();
		return count == 0 ? 1 : count++;
	}
}
