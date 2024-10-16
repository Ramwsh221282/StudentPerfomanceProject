using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.Validators;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.Errors;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.EducationPlans;

// Сущность. Учебный план.
public sealed class EducationPlan : Entity
{
	// Список семестров.
	private List<Semester> _semesters = [];

	// Список групп
	private List<StudentGroup> _groups = [];

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

	// Направление подготовки.
	public EducationDirection Direction { get; private set; } = null!;

	// Год набора.
	public YearOfCreation Year { get; private set; } = null!;

	// Список семестров read-only.
	public IReadOnlyCollection<Semester> Semesters => _semesters;

	// Список групп read-only.
	public IReadOnlyCollection<StudentGroup> Groups => _groups;

	// Добавление семестра
	public CSharpFunctionalExtensions.Result AddSemester(Semester semester)
	{
		if (semester == null)
			return CSharpFunctionalExtensions.Result.Failure(new SemesterNullError().ToString());

		if (Direction.Type == new BachelorDirection() && _semesters.Count > DirectionTypeConstraints.BachelorSemestersLimit)
			return CSharpFunctionalExtensions.Result.Failure(new EducationPlanSemestersLimitError(this).ToString());

		if (Direction.Type == new MagisterDirection() && _semesters.Count > DirectionTypeConstraints.MagisterSemestersLimit)
			return CSharpFunctionalExtensions.Result.Failure(new EducationPlanSemestersLimitError(this).ToString());

		if (_semesters.Any(s => s.Number == semester.Number))
			return CSharpFunctionalExtensions.Result.Failure(new EducationPlanSemesterDublicateError(this).ToString());

		_semesters.Add(semester);
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Регистрация студенческой группы
	public CSharpFunctionalExtensions.Result RegisterGroup(StudentGroup group)
	{
		if (group == null)
			return CSharpFunctionalExtensions.Result.Failure(new GroupNotFoundError().ToString());

		group.AttachEducationPlan(this);
		_groups.Add(group);
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Фабричный метод создания дефолтного объекта.
	public static EducationPlan CreateDefault() => new EducationPlan();

	// Фабричный метод создания учебного плана без семестров.
	public static CSharpFunctionalExtensions.Result<EducationPlan> Create(EducationDirection direction, YearOfCreation year)
	{
		EducationPlan plan = new EducationPlan(Guid.NewGuid(), direction, year);
		Validator<YearOfCreation> validator = new YearOfCreationValidator(year);
		return validator.Validate() ? plan : CSharpFunctionalExtensions.Result.Failure<EducationPlan>(validator.GetErrorText());
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
