using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
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
