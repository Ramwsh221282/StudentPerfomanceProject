using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters;

public sealed class Semester : Entity
{
	private List<SemesterPlan> _contracts = [];
	public Semester() : base(Guid.Empty)
	{
		Number = SemesterNumber.CreateDefault();
	}
	private Semester(Guid id, SemesterNumber number, EducationPlan plan) : base(id)
	{
		Number = number;
		Plan = plan;
	}
	public EducationPlan Plan { get; } = null!;
	public SemesterNumber Number { get; } = null!;
	public IReadOnlyCollection<SemesterPlan> Contracts => _contracts;
	public CSharpFunctionalExtensions.Result AddContract(SemesterPlan plan)
	{
		if (plan == null)
			return Failure(new SemesterPlanDisciplineNullError().ToString());
		if (_contracts.Any(c => c.Discipline == plan.Discipline))
			return Failure(new SemesterHasDisciplineAlreadyError(this).ToString());
		_contracts.Add(plan);
		return Success();
	}

	public CSharpFunctionalExtensions.Result RemoveContract(SemesterPlan plan)
	{
		if (plan == null)
			return Failure(new SemesterPlanDisciplineNullError().ToString());
		SemesterPlan? target = _contracts.FirstOrDefault(c => c.Discipline == plan.Discipline);
		if (target == null)
			return Failure(new SemesterHasNotDisciplineError(this).ToString());
		_contracts.Remove(target);
		return Success();
	}

	public static CSharpFunctionalExtensions.Result<Semester> Create(SemesterNumber number, EducationPlan plan)
	{
		Semester semester = new Semester(Guid.NewGuid(), number, plan);
		return semester;
	}
}
