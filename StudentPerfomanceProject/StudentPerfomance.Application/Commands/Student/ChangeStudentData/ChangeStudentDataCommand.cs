using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.Commands.Student.ChangeStudentData;

public sealed class ChangeStudentDataCommand : ICommand
{
	public StudentSchema Student { get; init; }
	public StudentsGroupSchema Group { get; init; }
	public ISchemaValidator StudentValidator { get; init; }
	public ISchemaValidator GroupValidator { get; init; }
	public ChangeStudentDataCommand(StudentSchema oldData, StudentsGroupSchema group)
	{
		Student = oldData;
		Group = group;
		StudentValidator = new StudentSchemaValidator()
		.WithNameValidation(oldData)
		.WithStateValidation(oldData)
		.WithRecordbookValidation(oldData);
		StudentValidator.ProcessValidation();
		GroupValidator = new StudentsGroupValidator(group);
		GroupValidator.ProcessValidation();
	}
}
