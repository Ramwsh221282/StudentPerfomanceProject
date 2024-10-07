using Microsoft.AspNetCore.Mvc;

using SPerfomance.Application.Shared.Module.Operations;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans;
using SPerfomance.Domain.Module.Shared.Entities.EducationPlans.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.EducationPlans;

public record EducationPlanSchema : EntitySchema
{
	public uint Year { get; init; }
	public EducationDirectionSchema Direction { get; init; } = new EducationDirectionSchema("", "", "");

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
		EducationDirectionSchema directionSchema = new EducationDirectionSchema(plan.Direction.Code.Code, plan.Direction.Name.Name, plan.Direction.Type.Type);
		EducationPlanSchema planSchema = new EducationPlanSchema(plan.Year.Year, directionSchema);
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
