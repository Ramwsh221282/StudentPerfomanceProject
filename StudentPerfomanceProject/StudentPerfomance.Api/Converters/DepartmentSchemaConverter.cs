using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.DataAccess.Repositories.TeachersDepartments;

namespace StudentPerfomance.Api.Converters;

public static class DepartmentSchemaConverter
{
	public static TeachersDepartmentRepositoryParameter ToRepositoryParameter(DepartmentSchema schema) =>
		new TeachersDepartmentRepositoryParameter()
			.WithName(schema.Name);
}
