using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
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
	public void AddContract(SemesterPlan plan)
	{
		if (plan.LinkedDiscipline == null) return;
		if (plan == null || _contracts.Any(c => c.LinkedDiscipline != null && c.LinkedDiscipline.Name == plan.LinkedDiscipline.Name))
			return;
		_contracts.Add(plan);
	}

	public void RemoveContract(SemesterPlan plan)
	{
		if (plan.LinkedDiscipline == null) return;
		SemesterPlan? target = _contracts.FirstOrDefault(c => c.LinkedDiscipline != null && c.LinkedDiscipline.Name == plan.LinkedDiscipline.Name);
		if (target == null)
			return;
		_contracts.Remove(target);
	}

	public static CSharpFunctionalExtensions.Result<Semester> Create(SemesterNumber number, EducationPlan plan)
	{
		Semester semester = new Semester(Guid.NewGuid(), number, plan);
		return semester;
	}
}
