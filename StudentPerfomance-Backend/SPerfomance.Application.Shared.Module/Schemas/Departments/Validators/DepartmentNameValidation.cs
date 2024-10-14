using CSharpFunctionalExtensions;
using SPerfomance.Domain.Module.Shared.Entities.TeacherDepartments;

namespace SPerfomance.Application.Shared.Module.Schemas.Departments.Validators;

internal sealed class DepartmentNameValidation(DepartmentSchema department) : BaseSchemaValidation, ISchemaValidation<DepartmentSchema>
{
	private readonly DepartmentSchema _schema = department;
	public string Error => errorBuilder.ToString();
	public Func<EntitySchema, bool> BuildCriteria(DepartmentSchema schema) => (schema) => Validate();
	protected override bool Validate()
	{
		Result<TeachersDepartment> result = TeachersDepartment.Create(_schema.Name);
		return result.IsFailure ? ReturnError(result.Error) : ReturnSuccess();
	}
}
