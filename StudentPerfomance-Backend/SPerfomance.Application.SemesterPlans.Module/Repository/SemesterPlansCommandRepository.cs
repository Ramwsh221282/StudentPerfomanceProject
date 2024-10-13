using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Extensions;

namespace SPerfomance.Application.SemesterPlans.Module.Repository;

internal sealed class SemesterPlansCommandRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();

	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<Result<SemesterPlan>> Create(
		SemesterPlanSchema plan,
		IRepositoryExpression<Semester> getSemester
	)
	{
		Semester? semester = await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.FirstOrDefaultAsync(getSemester.Build());

		if (semester == null)
			return Result.Failure<SemesterPlan>(new SemesterNotFoundError().ToString());

		SemesterPlan entry = plan.CreateDomainObject(semester);
		Result create = semester.AddContract(entry);
		if (create.IsFailure)
			return Result.Failure<SemesterPlan>(create.Error);

		entry.SetNumber(await GenerateEntityNumber());
		_context.Semesters.Attach(entry.Semester);
		await _context.SemesterPlans.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<SemesterPlan>> Remove(IRepositoryExpression<Semester> getSemester, IRepositoryExpression<SemesterPlan> getPlan)
	{
		Semester? semester = await _context.Semesters
		.Include(s => s.Plan)
		.ThenInclude(p => p.Direction)
		.Include(s => s.Contracts)
		.AsNoTracking()
		.FirstOrDefaultAsync(getSemester.Build());
		if (semester == null)
			return Result.Failure<SemesterPlan>(new SemesterNotFoundError().ToString());

		SemesterPlan? plan = await _context.SemesterPlans.FirstOrDefaultAsync(getPlan.Build());
		if (plan == null)
			return Result.Failure<SemesterPlan>(new SemesterPlanNotFoundError().ToString());

		Result remove = semester.RemoveContract(plan);
		if (remove.IsFailure)
			return Result.Failure<SemesterPlan>(remove.Error);

		await _context.SemesterPlans.Where(p => p.Id == plan.Id)
		.ExecuteDeleteAsync();
		await Commit();
		return plan;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int[] numbers = await _context.SemesterPlans.Select(s => s.EntityNumber).ToArrayAsync();
		return numbers.GetOrderedValue();
	}
}
