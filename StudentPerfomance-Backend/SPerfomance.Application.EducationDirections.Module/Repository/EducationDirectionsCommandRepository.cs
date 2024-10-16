using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.EducationDirections.Module.Repository;

internal sealed class EducationDirectionsCommandRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();

	public async Task Commit() => await _db.SaveChangesAsync();

	public async Task<Result<EducationDirection>> Create(EducationDirectionSchema direction, IRepositoryExpression<EducationDirection> expression)
	{
		if (await _db.EducationDirections.AnyAsync(expression.Build()))
			return Result.Failure<EducationDirection>(new EducationDirectionCodeDublicateError(direction.Code).ToString());

		EducationDirection entry = EducationDirection.Create(direction.CreateDirectionCode(), direction.CreateDirectionName(), direction.CreateDirectionType()).Value;
		entry.SetNumber(await GenerateEntityNumber());
		await _db.EducationDirections.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<EducationDirection>> Remove(IRepositoryExpression<EducationDirection> expression)
	{
		EducationDirection? direction = await _db.EducationDirections
		.Include(d => d.Plans)
		.ThenInclude(p => p.Groups)
		.ThenInclude(g => g.Students)
		.FirstOrDefaultAsync(expression.Build());
		if (direction == null)
			return Result.Failure<EducationDirection>(new EducationDirectionNotFoundError().ToString());

		foreach (var plan in direction.Plans)
		{
			foreach (var group in plan.Groups)
			{
				group.DeattachEducationPlan();
			}
		}
		await Commit();
		await _db.EducationDirections.Where(d => d.Id == direction.Id).ExecuteDeleteAsync();
		await Commit();
		return direction;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _db.EducationDirections.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
