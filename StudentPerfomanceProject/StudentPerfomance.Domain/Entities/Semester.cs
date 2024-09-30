using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.Semesters;
using StudentPerfomance.Domain.ValueObjects.SemesterValueObjects;

namespace StudentPerfomance.Domain.Entities;

public class Semester : Entity
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
		if (plan == null || _contracts.Any(c => c.LinkedDiscipline.Name == plan.LinkedDiscipline.Name))
			return;
		_contracts.Add(plan);
	}

	public void RemoveContract(SemesterPlan plan)
	{
		SemesterPlan? target = _contracts.FirstOrDefault(c => c.LinkedDiscipline.Name == plan.LinkedDiscipline.Name);
		if (target == null)
			return;
		_contracts.Remove(target);
	}

	public static Result<Semester> Create(SemesterNumber number, EducationPlan plan)
	{
		Semester semester = new Semester(Guid.NewGuid(), number, plan);
		Validator<SemesterNumber> validator = new SemesterNumberValidator(number);
		return validator.Validate() ? semester : Result.Failure<Semester>(validator.GetErrorText());
	}
}
