using SPerfomance.Api.Module.Converters.EducationPlans;
using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.EducationPlans;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;

namespace SPerfomance.Api.Module.Converters.Semesters;

public static class SemesterSchemaConverter
{
	public static SemestersRepositoryObject ToRepositoryObject(this SemesterSchema schema)
	{
		EducationPlansRepositoryObject plan = EducationPlanSchemaConverter.ToRepositoryObject(schema.Plan);
		SemestersRepositoryObject semester = new SemestersRepositoryObject()
		.WithNumber(schema.Number)
		.WithPlan(plan);
		return semester;
	}
}
