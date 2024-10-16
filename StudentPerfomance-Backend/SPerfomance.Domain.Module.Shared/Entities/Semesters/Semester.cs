using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Domain.Module.Shared.Entities.Semesters;

public sealed class Semester : Entity
{
	// Дисциплины семестра
	private List<SemesterPlan> _contracts = [];

	// Пустой конструктор создания дефолтного объекта для EF Core.
	public Semester() : base(Guid.Empty) => Number = SemesterNumber.CreateDefault();

	// Конструктор создания объекта с инициализацией
	private Semester(Guid id, SemesterNumber number, EducationPlan plan) : base(id)
	{
		Number = number;
		Plan = plan;
	}

	// Учебный план
	public EducationPlan Plan { get; } = null!;

	// Номер семестра
	public SemesterNumber Number { get; } = null!;

	// Дисциплины семестра read-only
	public IReadOnlyCollection<SemesterPlan> Contracts => _contracts;

	// Добавление дисциплины
	public CSharpFunctionalExtensions.Result AddContract(SemesterPlan plan)
	{
		if (plan == null)
			return Failure(new SemesterPlanDisciplineNullError().ToString());

		if (_contracts.Any(c => c.Discipline == plan.Discipline))
			return Failure(new SemesterHasDisciplineAlreadyError(this).ToString());

		_contracts.Add(plan);
		return Success();
	}

	// Удаление дисциплины
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

	// Найти дисциплину
	public CSharpFunctionalExtensions.Result<SemesterPlan> GetContract(string disciplineName)
	{
		if (string.IsNullOrWhiteSpace(disciplineName))
			return CSharpFunctionalExtensions.Result.Failure<SemesterPlan>(new SemesterPlanDisciplineNameEmptyError().ToString());

		SemesterPlan? desired = _contracts.FirstOrDefault(c => c.Discipline.Name == disciplineName);
		return desired ?? CSharpFunctionalExtensions.Result.Failure<SemesterPlan>(new SemesterPlanNotFoundError().ToString());
	}

	// Фабричный метод создания объекта
	public static CSharpFunctionalExtensions.Result<Semester> Create(SemesterNumber number, EducationPlan plan)
	{
		Semester semester = new Semester(Guid.NewGuid(), number, plan);
		return semester;
	}
}
