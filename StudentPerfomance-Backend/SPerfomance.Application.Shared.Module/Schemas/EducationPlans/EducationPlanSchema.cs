using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

public record EducationPlanSchema : EntitySchema
{
	public int EntityNumber { get; set; }
	public uint Year { get; init; }
	public EducationDirectionSchema Direction { get; init; } = new EducationDirectionSchema();
	public List<SemesterSchema> Semesters { get; set; } = new List<SemesterSchema>();

	public EducationPlanSchema() { }

	public EducationPlanSchema(uint year, EducationDirectionSchema? direction)
	{
		Year = year;
		if (direction != null) Direction = direction;
	}

	public EducationPlan CreateDomainObject(EducationDirection direction)
	{
		YearOfCreation year = YearOfCreation.Create(Year).Value;
		return EducationPlan.Create(direction, year).Value;
	}
}

public static class EducationPlanSchemaExtensions
{
	public static EducationPlansRepositoryObject ToRepositoryObject(this EducationPlanSchema schema)
	{
		EducationDirectionsRepositoryObject direction = schema.Direction.ToRepositoryObject();
		EducationPlansRepositoryObject plan = new EducationPlansRepositoryObject()
		.WithYear(schema.Year)
		.WithDirection(direction);
		return plan;
	}

	public static EducationPlanSchema ToSchema(this EducationPlan plan)
	{
		string code = plan.Direction.Code.Code; string name = plan.Direction.Name.Name; string type = plan.Direction.Type.Type;
		EducationDirectionSchema directionSchema = new EducationDirectionSchema(code, name, type);
		directionSchema.EntityNumber = plan.Direction.EntityNumber;
		EducationPlanSchema planSchema = new EducationPlanSchema(plan.Year.Year, directionSchema);
		List<Domain.Module.Shared.Entities.Semesters.Semester> semesters = plan.Semesters.ToList();
		List<SemesterSchema> semesterSchemas = semesters.Select(s => s.ToSchema()).ToList();
		foreach (var semester in semesterSchemas)
		{
			semester.SetEducationPlan(planSchema);
		}
		return planSchema;
	}

	public static ActionResult<EducationPlanSchema> ToActionResult(this OperationResult<EducationPlan> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.ToSchema());
	}

	public static ActionResult<IReadOnlyCollection<EducationPlanSchema>> ToActionResult(this OperationResult<IReadOnlyCollection<EducationPlan>> result)
	{
		return result.Result == null || result.IsFailed ?
			new BadRequestObjectResult(result.Error) :
			new OkObjectResult(result.Result.Select(ToSchema));
	}
}
