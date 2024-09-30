using CSharpFunctionalExtensions;

using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Entities.Teachers.ValueObjects;

namespace SPerfomance.Application.Shared.Module.Schemas.Teachers.Validators;

internal sealed class TeacherNameValidation(TeacherSchema teacher) : BaseSchemaValidation, ISchemaValidation<TeacherSchema>
{
	private readonly TeacherSchema _schema = teacher;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(TeacherSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<TeacherName> result = TeacherName.Create(_schema.Name, _schema.Surname, _schema.Thirdname);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
