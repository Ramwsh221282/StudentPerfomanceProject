using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.DataAccess.Repositories.StudentGroups;

namespace StudentPerfomance.Api.Converters;

public static class StudentsGroupSchemaConverter
{
	public static StudentGroupsRepositoryParameter ToRepositoryParameter(StudentsGroupSchema schema) =>
		new StudentGroupsRepositoryParameter()
			.WithName(schema.Name);
}
