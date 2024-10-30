using Microsoft.EntityFrameworkCore;

using SPerfomance.Domain.Models.Users;
using SPerfomance.Domain.Models.Users.Abstractions;
using SPerfomance.Domain.Tools;

namespace SPerfomance.DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
	private readonly DatabaseContext _context = new DatabaseContext();

	public async Task Insert(User entity)
	{
		entity.SetNumber(await GenerateEntityNumber());
		await _context.AddAsync(entity);
	}

	public async Task<int> Count() => await _context.Users.CountAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Users.Select(u => u.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}

	public async Task<User?> GetById(string Id) =>
		await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == Id.ToUpper());

	public async Task<IReadOnlyCollection<User>> GetFiltered(
		string? name,
		string? surname,
		string? patronymic,
		string? email,
		string? role,
		int page,
		int pageSize) =>
			await _context.Users
			.Where(u =>
				!string.IsNullOrWhiteSpace(name) && u.Name.Name.Contains(name) ||
				!string.IsNullOrWhiteSpace(surname) && u.Name.Surname.Contains(surname) ||
				!string.IsNullOrWhiteSpace(patronymic) && u.Name.Patronymic.Contains(patronymic) ||
				!string.IsNullOrWhiteSpace(email) && u.Email.Email.Contains(email) ||
				!string.IsNullOrWhiteSpace(role) && u.Role.Role.Contains(role))
			.Skip((page - 1) * pageSize)
			.Take(pageSize).AsNoTracking()
			.ToListAsync();

	public async Task<IReadOnlyCollection<User>> GetPaged(int page, int pageSize) =>
		await _context.Users
		.Skip((page - 1) * pageSize)
		.Take(pageSize).AsNoTracking()
		.ToListAsync();

	public async Task Remove(User entity) =>
		await _context.Users.Where(u => u.Id == entity.Id)
		.ExecuteDeleteAsync();

	public async Task Update(User entity) =>
		await _context.Users.Where(u => u.Id == entity.Id)
		.ExecuteUpdateAsync(u =>
		u.SetProperty(u => u.Name.Name, entity.Name.Name)
		.SetProperty(u => u.Name.Surname, entity.Name.Surname)
		.SetProperty(u => u.Name.Patronymic, entity.Name.Patronymic)
		.SetProperty(u => u.Email.Email, entity.Email.Email)
		.SetProperty(u => u.Role.Role, entity.Role.Role)
		.SetProperty(u => u.HashedPassword, entity.HashedPassword)
		.SetProperty(u => u.AttachedRoleId, entity.AttachedRoleId));

	public async Task<User?> GetByEmail(string? email) =>
		await _context.Users
		.AsNoTracking()
		.FirstOrDefaultAsync(u => u.Email.Email == email);

	public async Task<bool> HasWithEmail(string? email) => await _context.Users.AnyAsync(u => u.Email.Email == email);

	public async Task UpdateLoginDate(User entity) =>
		await _context.Users.Where(u => u.Id == entity.Id)
		.ExecuteUpdateAsync(u => u.SetProperty(u => u.LastLoginDate, entity.LastLoginDate));
}
