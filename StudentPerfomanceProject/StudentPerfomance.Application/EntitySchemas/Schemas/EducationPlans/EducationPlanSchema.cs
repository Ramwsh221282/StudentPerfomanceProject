using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;

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
}
