using StudentPerfomance.Application.EntitySchemas.Schemas.EducationPlans;
using StudentPerfomance.DataAccess.Repositories.EducationPlans;

namespace StudentPerfomance.Api.Converters;

public static class EducationPlanSchemaConverter
{
	public static EducationPlanRepositoryParameter ToRepositoryParameter(EducationPlanSchema schema) =>
		new EducationPlanRepositoryParameter().WithYear(schema.Year);
}
