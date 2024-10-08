using Microsoft.EntityFrameworkCore;

using CSharpFunctionalExtensions;

using SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;
using SPerfomance.DataAccess.Module.Shared;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;

namespace SPerfomance.Application.SemesterPlans.Module.Repository;

internal sealed class SemesterPlansCommandRepository
{
	private readonly ApplicationDb _context = new ApplicationDb();
	public async Task Commit() => await _context.SaveChangesAsync();

	public async Task<Result<SemesterPlan>> Create(SemesterPlanSchema plan, IRepositoryExpression<Semester> getSemester, IRepositoryExpression<SemesterPlan> findDublicate)
	{
		if (await _context.SemesterPlans.AnyAsync(findDublicate.Build()))
			return Result.Failure<SemesterPlan>(new SemesterPlanDublicateError(plan.Semester.Number, plan.Semester.Plan.Year, plan.Semester.Plan.Direction.Name, plan.DisciplineName).ToString());
		Semester? semester = await _context.Semesters.FirstOrDefaultAsync(getSemester.Build());
		if (semester == null)
			return Result.Failure<SemesterPlan>(new SemesterNotFoundError().ToString());
		SemesterPlan entry = plan.CreateDomainObject(semester);
		Result create = semester.AddContract(entry);
		if (create.IsFailure)
			return Result.Failure<SemesterPlan>(create.Error);
		_context.Semesters.Attach(entry.Semester);
		await _context.SemesterPlans.AddAsync(entry);
		await Commit();
		return entry;
	}

	public async Task<Result<SemesterPlan>> Remove(IRepositoryExpression<SemesterPlan> getPlan)
	{
		SemesterPlan? plan = await _context.SemesterPlans.FirstOrDefaultAsync(getPlan.Build());
		if (plan == null)
			return Result.Failure<SemesterPlan>(new SemesterPlanNotFoundError().ToString());
		await _context.SemesterPlans.Where(p => p.Id == plan.Id)
		.ExecuteDeleteAsync();
		await Commit();
		return plan;
	}

	public async Task<int> GenerateEntityNumber()
	{
		int count = await _context.SemesterPlans.CountAsync();
		return count + 1;
	}
}
