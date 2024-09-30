using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationDirections;

namespace SPerfomance.Api.Module.Converters.EducationDirections;

public static class EducationDirectionSchemaConverter
{
	public static EducationDirectionsRepositoryObject ToRepositoryObject(EducationDirectionSchema schema)
	{
		EducationDirectionsRepositoryObject direction = new EducationDirectionsRepositoryObject()
		.WithDirectionName(schema.Name)
		.WithDirectionType(schema.Type)
		.WithDirectionCode(schema.Code);
		return direction;
	}
}
