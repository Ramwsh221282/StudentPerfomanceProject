using System.Text;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.Entities;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntityValidators;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Errors;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.Validators;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Teachers;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.Errors;

namespace SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;

// План семестра. Связка дисциплина + семестр.
public sealed class SemesterPlan : Entity
{
	// Базовый конструктор для видимости EF Core.
	// Инициализирует объект со стандартным значением.
	public SemesterPlan() : base(Guid.Empty) => Discipline = Discipline.CreateDefault();

	// Конструктор построения объекта с инициализацией параметров.
	private SemesterPlan(Guid id, Semester semester, Discipline discipline) : base(id)
	{
		Semester = semester;
		Discipline = discipline;
	}

	// Семестр, к которому привязан план семестра.
	public Semester Semester { get; } = null!;
	// Дисциплина плана семестра.
	public Discipline Discipline { get; private set; } = null!;
	// Фабричный метод создания объекта с процедурой валидации.
	public Teacher? AttachedTeacher { get; private set; }
	public static CSharpFunctionalExtensions.Result<SemesterPlan> Create(Semester semester, Discipline discipline)
	{
		if (semester == null || discipline == null)
			return CSharpFunctionalExtensions.Result.Failure<SemesterPlan>(new SemesterPlanError().ToString());
		return new SemesterPlan(Guid.NewGuid(), semester, discipline);
	}

	// Метод изменяет обновляет информацию о дисциплине (её название).
	public CSharpFunctionalExtensions.Result UpdateDiscipline(Discipline discipline)
	{
		if (discipline == null)
			return CSharpFunctionalExtensions.Result.Failure(new SemesterPlanDisciplineNullError().ToString());
		Validator<Discipline> validator = new SemesterPlanDisciplineValidator(discipline);
		if (!validator.Validate())
			return CSharpFunctionalExtensions.Result.Failure(validator.GetErrorText());
		Discipline = discipline;
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Открепление преподавателя за дисциплиной.
	public CSharpFunctionalExtensions.Result DeattachTeacher()
	{
		if (AttachedTeacher == null)
			return CSharpFunctionalExtensions.Result.Failure(new TeacherNullError().ToString());
		AttachedTeacher = null;
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Закрепление преподавателя за дисциплиной.
	public CSharpFunctionalExtensions.Result AttachTeacher(Teacher? teacher)
	{
		if (teacher == null)
			return CSharpFunctionalExtensions.Result.Failure(new TeacherNullError().ToString());
		if (AttachedTeacher != null)
			return CSharpFunctionalExtensions.Result.Failure(new TeacherAlreadyAttachedError(this).ToString());
		AttachedTeacher = teacher;
		return CSharpFunctionalExtensions.Result.Success();
	}

	// Переопределение ToString() для вывода информации об объекте (тестовая область).
	public override string ToString()
	{
		StringBuilder infoBuilder = new StringBuilder();
		infoBuilder.AppendLine($"Семестр: {Semester.Number.Value}");
		infoBuilder.AppendLine($"Дисциплина: {Discipline.Name}");
		return infoBuilder.ToString();
	}
}
