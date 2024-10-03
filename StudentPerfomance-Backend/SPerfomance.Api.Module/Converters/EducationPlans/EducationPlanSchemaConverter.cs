using SPerfomance.Api.Module.Converters.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;

namespace SPerfomance.Api.Module.Converters.EducationPlans;

public static class EducationPlanSchemaConverter
{
	public static EducationPlansRepositoryObject ToRepositoryObject(this EducationPlanSchema schema)
	{
		EducationDirectionsRepositoryObject direction = EducationDirectionSchemaConverter.ToRepositoryObject(schema.Direction);
		EducationPlansRepositoryObject plan = new EducationPlansRepositoryObject()
		.WithYear(schema.Year)
		.WithDirection(direction);
		return plan;
	}
}
