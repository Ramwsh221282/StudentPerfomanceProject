using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.ValueObjects;

namespace SPerfomance.Application.Shared.Users.Module.Repositories;

public sealed class UsersQueryRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();

	public async Task Commit() => await _db.SaveChangesAsync();

	public async Task<bool> HasEqualRecord(IRepositoryExpression<User> expression) =>
		await _db.Users.AnyAsync(expression.Build());

	public async Task<bool> HasEqualRecord(IRepositoryExpression<Teacher> expression) =>
		await _db.Teachers.AnyAsync(expression.Build());

	public async Task<User?> GetByParameter(IRepositoryExpression<User> expression) =>
		await _db.Users.FirstOrDefaultAsync(expression.Build());

	public async Task<Teacher?> GetByParmaeter(IRepositoryExpression<Teacher> expression) =>
		await _db.Teachers.FirstOrDefaultAsync(expression.Build());

	public async Task<IReadOnlyCollection<User>> GetPaged(int page, int pageSize) =>
		await _db.Users.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.OrderBy(u => u.EntityNumber)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<User>> GetFilteredAndPaged(IRepositoryExpression<User> expression, int page, int pageSize) =>
		await _db.Users.Skip((page - 1) * pageSize)
		.Take(pageSize)
		.Where(expression.Build())
		.OrderBy(u => u.EntityNumber)
		.AsNoTracking()
		.ToListAsync();

	public async Task<IReadOnlyCollection<User>> GetFiltered(IRepositoryExpression<User> expression) =>
		await _db.Users
		.Where(expression.Build())
		.OrderBy(u => u.EntityNumber)
		.AsNoTracking()
		.ToListAsync();

	public async Task<User?> GetById(string id) =>
		await _db.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id.ToUpper());

	public async Task<int> Count() => await _db.Users.CountAsync();
}
