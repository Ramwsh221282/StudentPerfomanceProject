using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Domain.ValueObjects.Student;

namespace StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

public sealed class StudentNameValidation(StudentSchema schema) : BaseSchemaValidation, ISchemaValidation<StudentSchema>
{
	private readonly StudentSchema _schema = schema;
	public string Error => errorBuilder.ToString();

	public Func<EntitySchema, bool> BuildCriteria(StudentSchema schema) =>
		(schema) => Validate();

	protected override bool Validate()
	{
		Result<StudentName> request = StudentName.Create(_schema.Name, _schema.Surname, _schema.Thirdname);
		if (request.IsFailure)
		{
			AppendErrorText(request.Error);
			return false;
		}
		return true;
	}
}
