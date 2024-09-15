using CSharpFunctionalExtensions;

using StudentPerfomance.Application.EntitySchemas.Schemas;
using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Domain.Entities;

namespace StudentPerfomance.Application.EntitySchemas.Validators.DepartmentSchemaValidation;

public sealed class DepartmentNameValidation(DepartmentSchema schema) : BaseSchemaValidation, ISchemaValidation<DepartmentSchema>
{
	private readonly DepartmentSchema _schema = schema;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(DepartmentSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<TeachersDepartment> result = TeachersDepartment.Create(Guid.Empty, _schema.Name);
		if (result.IsFailure)
			return ReturnError(result.Error);
		return ReturnSuccess();
	}
}
