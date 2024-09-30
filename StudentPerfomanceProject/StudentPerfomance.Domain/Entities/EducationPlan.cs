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
	// Добавление семестра
	public void AddSemester(Semester semester) => _semesters.Add(semester);
	// Фабричный метод создания дефолтного объекта.
	public static EducationPlan CreateDefault() => new EducationPlan();
	// Фабричный метод создания учебного плана без семестров.
	public static Result<EducationPlan> Create(EducationDirection direction, YearOfCreation year)
	{
		EducationPlan plan = new EducationPlan(Guid.NewGuid(), direction, year);
		Validator<YearOfCreation> validator = new YearOfCreationValidator(year);
		return validator.Validate() ? plan : Result.Failure<EducationPlan>(validator.GetErrorText());
	}

	public override bool Equals(object? obj)
	{
		if (obj == null) return false;
		if (obj is EducationPlan other)
		{
			return other.Year == Year &&
				   other.Direction.Name == Direction.Name &&
				   other.Direction.Code == Direction.Code &&
				   other.Direction.Type == Direction.Type;
		}
		return false;
	}

	public override int GetHashCode() => GetHashCode();
}
