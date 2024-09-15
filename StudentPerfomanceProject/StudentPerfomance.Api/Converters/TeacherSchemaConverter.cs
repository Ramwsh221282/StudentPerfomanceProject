using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.DataAccess.Repositories.Teachers;

namespace StudentPerfomance.Api.Converters;

public static class TeacherSchemaConverter
{
	public static TeacherRepositoryParameter ToRepositoryParameter(TeacherSchema schema) =>
		new TeacherRepositoryParameter()
			.WithName(schema.Name)
			.WithSurname(schema.Surname)
			.WithThirdname(schema.Thirdname);
}
