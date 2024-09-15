using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.DataAccess.Repositories.Semesters;

namespace StudentPerfomance.Api.Converters;

public static class SemesterSchemaConverter
{
	public static SemestersRepositoryParameter ToRepositoryParameter(SemesterSchema schema) =>
		new SemestersRepositoryParameter()
			.WithNumber(schema.Number);
}
