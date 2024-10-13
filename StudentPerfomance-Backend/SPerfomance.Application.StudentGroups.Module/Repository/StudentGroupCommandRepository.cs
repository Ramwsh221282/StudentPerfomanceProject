using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.StudentGroups.Module.Repository;

internal sealed class StudentGroupCommandRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task<Result<StudentGroup>> Create(StudentsGroupSchema group, IRepositoryExpression<StudentGroup> findDublicate)
	{
		if (await _context.Groups.AnyAsync(findDublicate.Build()))
			return Result.Failure<StudentGroup>(new GroupDublicateNameError(group.Name).ToString());
		StudentGroup entry = group.CreateDomainObject();
		entry.SetNumber(await GenerateEntityNumber());
		await _context.Groups.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<StudentGroup>> Remove(IRepositoryExpression<StudentGroup> getGroup)
	{
		StudentGroup? group = await _context.Groups.FirstOrDefaultAsync(getGroup.Build());
		if (group == null) return Result.Failure<StudentGroup>(new GroupNotFoundError().ToString());
		await _context.Groups.Where(g => g.Id == group.Id).ExecuteDeleteAsync();
		await Commit();
		return group;
	}

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.Groups.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
