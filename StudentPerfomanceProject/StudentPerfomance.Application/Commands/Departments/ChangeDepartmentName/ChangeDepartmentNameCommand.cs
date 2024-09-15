using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Validators.DepartmentSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.Commands.Departments.ChangeDepartmentName;

internal sealed class ChangeDepartmentNameCommand : ICommand
{
	public DepartmentSchema NewData { get; init; }
	public ISchemaValidator Validator { get; init; }

	public ChangeDepartmentNameCommand(DepartmentSchema schema)
	{
		NewData = schema;
		Validator = new DepartmentValidator(schema);
		Validator.ProcessValidation();
	}
}
