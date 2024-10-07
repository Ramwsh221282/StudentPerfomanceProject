using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Repository;

internal sealed class EducationDirectionsQueryRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();
	public async Task Commit() => await _db.SaveChangesAsync();
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
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	public async Task<IReadOnlyCollection<EducationDirection>> GetPaged(int page, int pageSize) =>
		await _db.EducationDirections
		.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.AsNoTracking()
		.ToListAsync();
	public async Task<bool> HasEqualRecord(IRepositoryExpression<EducationDirection> expression) =>
		await _db.EducationDirections.AnyAsync(expression.Build());
	public async Task<int> Count() => await _db.EducationDirections.CountAsync();
	public async Task<int> CountWithExpression(IRepositoryExpression<EducationDirection> expression) =>
		await _db.EducationDirections.CountAsync(expression.Build());
}
