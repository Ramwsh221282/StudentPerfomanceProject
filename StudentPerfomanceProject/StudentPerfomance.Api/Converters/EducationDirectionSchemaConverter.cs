using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.DataAccess.Repositories.EducationDirections;

namespace StudentPerfomance.Api.Converters;

public static class EducationDirectionSchemaConverter
{
	public static EducationDirectionRepositoryParameter ToRepositoryParameter(EducationDirectionSchema schema) =>
		new EducationDirectionRepositoryParameter().WithDirectionName(schema.Name).WithDirectionType(schema.Type).WithDirectionCode(schema.Code);
}
