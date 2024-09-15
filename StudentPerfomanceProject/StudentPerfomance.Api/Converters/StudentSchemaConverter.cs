using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.DataAccess.Repositories.Students;

namespace StudentPerfomance.Api.Converters;

public static class StudentSchemaConverter
{
	public static StudentsRepositoryParameter ToRepositoryParameter(StudentSchema schema) =>
		new StudentsRepositoryParameter()
			.WithName(schema.Name)
			.WithSurname(schema.Surname)
			.WithThirdname(schema.Thirdname)
			.WithState(schema.State)
			.WithRecordbook(schema.Recordbook);
}
