using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Domain.ValueObjects.Teacher;

namespace StudentPerfomance.Application.EntitySchemas.Validators.TeachersValidation;

public sealed class TeacherNameValidation(TeacherSchema schema) : BaseSchemaValidation, ISchemaValidation<TeacherSchema>
{
	private readonly TeacherSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(TeacherSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<TeacherName> result = TeacherName.Create(_schema.Name, _schema.Surname, _schema.Thirdname);
		if (result.IsFailure)
		{
			AppendErrorText(result.Error);
			return false;
		}
		return true;
	}
}
