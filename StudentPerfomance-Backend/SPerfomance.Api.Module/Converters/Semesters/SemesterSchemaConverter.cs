using SPerfomance.Application.Shared.Module.Schemas.Semesters;
using SPerfomance.DataAccess.Module.Shared.Repositories.Semesters;

namespace SPerfomance.Api.Module.Converters.Semesters;

public static class SemesterSchemaConverter
{
	public static SemestersRepositoryObject ToRepositoryParameter(SemesterSchema schema) =>
		new SemestersRepositoryObject()
			.WithNumber(schema.Number)
			.WithPlanYear(schema.PlanYear);
}
