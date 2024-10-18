using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Entities.Users;
using SPerfomance.Domain.Module.Shared.Entities.Users.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.Shared.Users.Module.Repositories;

internal sealed class UsersCommandRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();

	public async Task<Result<User>> Create(User user)
	{
		if (await _db.Users.AnyAsync(u => u.Email == user.Email))
			return Result.Failure<User>(new EmailIsNotFreeError(user.Email.Email).ToString());

		user.SetNumber(await GenerateEntityNumber());
		await _db.Users.AddAsync(user);
		await Commit();
		return user;
	}

	public async Task<Result<User>> CreateAsTeacher(User user)
	{
		if (await _db.Users.AnyAsync(u => u.Email == user.Email))
			return Result.Failure<User>(new EmailIsNotFreeError(user.Email.Email).ToString());

		if (!await _db.Teachers.AnyAsync(
			t => t.Name.Name == user.Name.Name &&
			t.Name.Surname == user.Name.Surname &&
			t.Name.Thirdname == user.Name.Thirdname
			))
			return Result.Failure<User>(new CantRegisterAsTeacherError().ToString());

		user.SetNumber(await GenerateEntityNumber());
		await _db.Users.AddAsync(user);
		await Commit();
		return user;
	}

	public async Task<Result<User>> Remove(User user)
	{
		await _db.Users.Where(u => u.Id == user.Id).ExecuteDeleteAsync();
		await Commit();
		return user;
	}

	public async Task Commit() => await _db.SaveChangesAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _db.Users.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
