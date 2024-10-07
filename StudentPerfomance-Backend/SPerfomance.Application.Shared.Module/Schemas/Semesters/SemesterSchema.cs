using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.Semesters;
using SPerfomance.Domain.Module.Shared.Entities.Semesters.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Semesters;

public record SemesterSchema : EntitySchema
{
	public byte Number { get; init; } = 0;
	public EducationPlanSchema Plan { get; init; } = new EducationPlanSchema();
	public SemesterSchema() { }
	public SemesterSchema(byte number, EducationPlanSchema? plan)
	{
		if (number > 0) Number = number;
		if (plan != null) Plan = plan;
	}
	public SemesterNumber CreateNumber() => SemesterNumber.Create(Number).Value;
	public Semester CreateDomainObject(EducationPlan plan) => Semester.Create(CreateNumber(), plan).Value;
}

public static class SemesterSchemaExtensions
{
	public static SemestersRepositoryObject ToRepositoryObject(this SemesterSchema schema)
	{
		SemestersRepositoryObject semester = new SemestersRepositoryObject()
		.WithNumber(schema.Number)
		.WithPlan(schema.Plan.ToRepositoryObject());
		return semester;
	}

	public static SemesterSchema ToSchema(this Semester semester)
	{
		SemesterSchema schema = new SemesterSchema(semester.Number.Value, semester.Plan.ToSchema());
		return schema;
	}

	public static ActionResult<SemesterSchema> ToActionResult(this OperationResult<Semester> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<SemesterSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<Semester>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
