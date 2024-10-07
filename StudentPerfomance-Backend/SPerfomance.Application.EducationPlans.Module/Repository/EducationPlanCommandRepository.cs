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

namespace SPerfomance.Application.EducationPlans.Module.Repository;

internal sealed class EducationPlanCommandRepository
{
	private readonly ApplicationDb _db = new ApplicationDb();
	public async Task<Result<EducationPlan>> Create(EducationPlanSchema plan, IRepositoryExpression<EducationDirection> getDirection, IRepositoryExpression<EducationPlan> findDublicate)
	{
		if (await _db.EducationPlans.AnyAsync(findDublicate.Build())) return Result.Failure<EducationPlan>(new EducationPlanDublicateError().ToString());
		EducationDirection? direction = await _db.EducationDirections.FirstOrDefaultAsync(getDirection.Build());
		if (direction == null) return Result.Failure<EducationPlan>(new EducationDirectionNotFoundError().ToString());
		EducationPlan entry = plan.CreateDomainObject(direction);
		_db.EducationDirections.Attach(entry.Direction);
		entry.SetNumber(await GenerateEntityNumber());
		ICreateEducationPlanPolicy policy = new CreateEducationPlanPolicy(entry);
		await policy.ExecutePolicy();
		await _db.EducationPlans.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<EducationPlan>> Remove(IRepositoryExpression<EducationPlan> getPlan)
	{
		EducationPlan? plan = await _db.EducationPlans.FirstOrDefaultAsync(getPlan.Build());
		if (plan == null) return Result.Failure<EducationPlan>(new EducationPlanNotFoundError().ToString());
		foreach (var semester in plan.Semesters)
		{
			await _db.SemesterPlans.Where(p => p.Semester.Id == semester.Id).ExecuteDeleteAsync();
		}
		await _db.Semesters.Where(s => s.Plan.Id == plan.Id).ExecuteDeleteAsync();
		await _db.EducationPlans.Where(p => p.Id == plan.Id).ExecuteDeleteAsync();
		await Commit();
		return plan;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int count = await _db.EducationPlans.CountAsync();
		return count + 1;
	}
	public async Task Commit() => await _db.SaveChangesAsync();
}
