using StudentPerfomance.Application.EntitySchemas.Schemas.TeacherDepartments;
using StudentPerfomance.Application.EntitySchemas.Validators.DepartmentSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.Commands.Departments.CreateDepartment;

internal sealed class CreateDepartmentCommand : ICommand
{
	public DepartmentSchema Department { get; init; }
	public ISchemaValidator Validator { get; init; }
	public CreateDepartmentCommand(DepartmentSchema schema)
	{
		Department = schema;
		Validator = new DepartmentValidator(schema);
		Validator.ProcessValidation();
	}
}
