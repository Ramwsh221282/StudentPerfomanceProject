using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.Application.Shared.Module.Schemas.Teachers;
using SPerfomance.DataAccess.Module.Shared.Repositories.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans;
using SPerfomance.Domain.Module.Shared.Entities.SemesterPlans.ValueObjects;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;

namespace SPerfomance.Application.Shared.Module.Schemas.SemesterPlans;

// DTO объект SemesterPlan.
public sealed record SemesterPlanSchema : EntitySchema
{
	// Название дисциплины.
	public string Discipline { get; init; } = string.Empty;

	// Закрепленный преподаватель
	public TeacherSchema Teacher { get; init; } = new TeacherSchema();

	// Создание стандартного DTO.
	public SemesterPlanSchema() { }
	// Создание DTO с инициализацией объекта при условии.
	public SemesterPlanSchema(string? disciplineName, TeacherSchema? teacher)
	{
		if (!string.IsNullOrWhiteSpace(disciplineName)) Discipline = disciplineName;
		if (teacher != null) Teacher = teacher;
	}

	// Создание Value Object Discipline.
	public Discipline CreateDiscipline() => Domain.Module.Shared.Entities.SemesterPlans.ValueObjects.Discipline.Create(Discipline).Value;
	// Создание Domain Object SemesterPlan.
	public SemesterPlan CreateDomainObject(Semester semester) => SemesterPlan.Create(semester, CreateDiscipline()).Value;
}

public static class SemesterPlanSchemaExtensions
{
	public static SemesterPlanRepositoryObject ToRepositoryObject(this SemesterPlanSchema schema)
	{
		SemesterPlanRepositoryObject parameter = new SemesterPlanRepositoryObject()
		.WithDisciplineName(schema.Discipline)
		.WithTeacher(schema.Teacher.ToRepositoryObject());
		return parameter;
	}

	public static SemesterPlanSchema ToSchema(this SemesterPlan plan)
	{
		TeacherSchema teacherSchema = plan.AttachedTeacher == null ? new TeacherSchema() : plan.AttachedTeacher.ToSchema();
		SemesterPlanSchema schema = new SemesterPlanSchema(plan.Discipline.Name, teacherSchema);
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
