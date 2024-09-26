using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.ValueObjects.EducationPlans;

namespace StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;

public record EducationPlanSchema : EntitySchema
{
	public uint Year { get; init; }
	public EducationDirectionSchema Direction { get; init; } = new EducationDirectionSchema("", "", "");

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
