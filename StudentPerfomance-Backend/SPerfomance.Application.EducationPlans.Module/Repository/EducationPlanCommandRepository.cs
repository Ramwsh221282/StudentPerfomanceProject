using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.Errors;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Application.EducationPlans.Module.Commands.Create.CreatePolicy;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.EducationPlans.Module.Repository;

internal sealed class EducationPlanCommandRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();

	public async Task<Result<EducationPlan>> Create(EducationPlanSchema plan, IRepositoryExpression<EducationDirection> getDirection)
	{
		EducationDirection? direction = await _db.EducationDirections
		.Include(d => d.Plans)
		.ThenInclude(p => p.Semesters)
		.ThenInclude(s => s.Contracts)
		.ThenInclude(c => c.AttachedTeacher)
		.FirstOrDefaultAsync(getDirection.Build());
		if (direction == null)
			return Result.Failure<EducationPlan>(new EducationDirectionNotFoundError().ToString());

		EducationPlan entry = plan.CreateDomainObject(direction);
		entry.SetNumber(await GenerateEntityNumber());
		Result add = direction.AddPlan(entry);
		if (add.IsFailure)
			return Result.Failure<EducationPlan>(add.Error);
		ICreateEducationPlanPolicy policy = new CreateEducationPlanPolicy(entry);
		await policy.ExecutePolicy();
		await _db.EducationPlans.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<EducationPlan>> Remove(IRepositoryExpression<EducationPlan> getPlan)
	{
		EducationPlan? plan = await _db.EducationPlans
		.Include(p => p.Direction)
		.Include(p => p.Groups)
		.ThenInclude(g => g.Students)
		.FirstOrDefaultAsync(getPlan.Build());
		if (plan == null)
			return Result.Failure<EducationPlan>(new EducationPlanNotFoundError().ToString());

		foreach (var group in plan.Groups)
		{
			group.DeattachEducationPlan();
		}
		await Commit();
		await _db.EducationPlans.Where(p => p.Id == plan.Id).ExecuteDeleteAsync();
		await Commit();
		return plan;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _db.EducationPlans.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}

	public async Task Commit() => await _db.SaveChangesAsync();
}
