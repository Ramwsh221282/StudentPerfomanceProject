using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;

namespace StudentPerfomance.Application.Commands.Student.CreateStudent;

public sealed class CreateStudentCommand : ICommand
{
	public ISchemaValidator StudentValidation { get; init; }
	public ISchemaValidator GroupValidation { get; init; }
	public StudentSchema Student { get; init; }
	public StudentsGroupSchema Group { get; init; }

	public CreateStudentCommand(StudentSchema student, StudentsGroupSchema group)
	{
		Student = student;
		Group = group;
		StudentValidation = new StudentSchemaValidator()
		.WithNameValidation(student)
		.WithStateValidation(student)
		.WithRecordbookValidation(student);
		GroupValidation = new StudentsGroupValidator(group);
		StudentValidation.ProcessValidation();
		GroupValidation.ProcessValidation();
	}
}
