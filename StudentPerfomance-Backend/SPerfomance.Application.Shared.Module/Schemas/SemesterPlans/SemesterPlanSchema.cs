using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
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
	public SemesterSchema Semester { get; init; } = new SemesterSchema();
	public SemesterPlanSchema() { }
	// Создание DTO с инициализацией объекта при условии.
	public SemesterPlanSchema(string? disciplineName, SemesterSchema? semester)
	{
		if (!string.IsNullOrWhiteSpace(disciplineName)) DisciplineName = disciplineName;
		if (semester != null) Semester = semester;
	}
	// Создание Value Object Discipline.
	public Discipline CreateDiscipline() => Discipline.Create(DisciplineName).Value;
	// Создание Domain Object SemesterPlan.
	public SemesterPlan CreateDomainObject(Semester semester) => SemesterPlan.Create(semester, CreateDiscipline()).Value;
}

public static class SemesterPlanSchemaExtensions
{
	public static SemesterPlanRepositoryObject ToRepositoryObject(this SemesterPlanSchema schema)
	{
		SemesterPlanRepositoryObject parameter = new SemesterPlanRepositoryObject()
		.WithDisciplineName(schema.DisciplineName)
		.WithSemester(schema.Semester.ToRepositoryObject());
		return parameter;
	}

	public static SemesterPlanSchema ToSchema(this SemesterPlan plan)
	{
		SemesterPlanSchema schema = new SemesterPlanSchema(plan.Discipline.Name, plan.Semester.ToSchema());
		return schema;
	}

	public static ActionResult<SemesterPlanSchema> ToActionResult(this OperationResult<SemesterPlan> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<SemesterPlanSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<SemesterPlan>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
