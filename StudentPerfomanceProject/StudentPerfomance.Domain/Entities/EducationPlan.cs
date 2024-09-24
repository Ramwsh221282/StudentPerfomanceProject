using CSharpFunctionalExtensions;

using StudentPerfomance.Domain.Validators;
using StudentPerfomance.Domain.Validators.EducationPlans;
using StudentPerfomance.Domain.ValueObjects.EducationPlans;

namespace StudentPerfomance.Domain.Entities;

// Сущность. Учебный план.
public sealed class EducationPlan : Entity
{
	// Список семестров.
	private List<Semester> _semesters = [];
	// Конструктор создания дефолтного объекта.
	private EducationPlan() : base(Guid.Empty)
	{
		Year = YearOfCreation.CreateDefault();
		Direction = EducationDirection.CreateDefault();
	}
	// Конструктор создания объекта без семестров.
	private EducationPlan(Guid id, EducationDirection direction, YearOfCreation year) : base(id)
	{
		Direction = direction;
		Year = year;
	}
	// Конструктор создания объекта с семестрами.
	private EducationPlan(Guid id, EducationDirection direction, YearOfCreation year, List<Semester> semesters) : base(id)
	{
		Direction = direction;
		Year = year;
		_semesters = semesters;
	}
	// Направление подготовки.
	public EducationDirection Direction { get; private set; } = null!;
	// Год набора.
	public YearOfCreation Year { get; private set; } = null!;
	// Получение списка семестров только для чтения.
	public IReadOnlyCollection<Semester> Semesters => _semesters;
	// Фабричный метод создания дефолтного объекта.
	public static EducationPlan CreateDefault() => new EducationPlan();
	// Фабричный метод создания учебного плана без семестров.
	public static Result<EducationPlan> Create(EducationDirection direction, YearOfCreation year)
	{
		EducationPlan plan = new EducationPlan(Guid.NewGuid(), direction, year);
		Validator<EducationPlan> validator = new EducationPlanValidator(plan);
		return validator.Validate() ? plan : Result.Failure<EducationPlan>(validator.GetErrorText());
	}
	// Фабричный метод создания учебного плана с семестрами.
	public static Result<EducationPlan> CreateWithSemesters(EducationDirection direction, YearOfCreation year, List<Semester> semesters)
	{
		EducationPlan plan = new EducationPlan(Guid.NewGuid(), direction, year, semesters);
		Validator<EducationPlan> validator = new EducationPlanValidator(plan);
		return validator.Validate() ? plan : Result.Failure<EducationPlan>(validator.GetErrorText());
	}
}
