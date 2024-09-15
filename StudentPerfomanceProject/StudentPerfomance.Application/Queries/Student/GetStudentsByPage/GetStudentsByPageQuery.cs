using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Application.Queries.Student.GetStudentsByPage;

internal sealed class GetStudentsByPageQuery : IQuery
{
	public int Page { get; init; }
	public int PageSize { get; init; }
	public ISchemaValidator Validator { get; init; }

	public GetStudentsByPageQuery(int page, int pageSize, StudentsGroupSchema schema)
	{
		Page = page;
		PageSize = pageSize;
		Validator = new StudentsGroupValidator(schema);
		Validator.ProcessValidation();
	}
}
