using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;

// DTO объект SemesterPlan.
public sealed record SemesterPlanSchema : EntitySchema
{
	// Название дисциплины.
	public string DisciplineName { get; init; } = string.Empty;
	// Создание стандартного DTO.
	public SemesterPlanSchema() { }
	// Создание DTO с инициализацией объекта при условии.
	public SemesterPlanSchema(string? disciplineName)
	{
		if (!string.IsNullOrWhiteSpace(disciplineName)) DisciplineName = disciplineName;
	}
	// Создание Value Object Discipline.
	public Discipline CreateDiscipline() => Discipline.Create(DisciplineName).Value;
	// Создание Domain Object SemesterPlan.
	public SemesterPlan CreateDomainObject(Semester semester) => SemesterPlan.Create(semester, CreateDiscipline()).Value;
}
