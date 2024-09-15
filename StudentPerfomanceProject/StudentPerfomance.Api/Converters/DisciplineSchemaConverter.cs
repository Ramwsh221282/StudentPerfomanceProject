using StudentPerfomance.Application.EntitySchemas.Schemas.Discplines;
using StudentPerfomance.DataAccess.Repositories.Disciplines;

namespace StudentPerfomance.Api.Converters;

public static class DisciplineSchemaConverter
{
	public static DisciplineRepositoryParameter ToRepositoryParameter(DisciplineSchema schema) =>
		new DisciplineRepositoryParameter()
			.WithName(schema.Name);
}
