using StudentPerfomance.Application.EntitySchemas.Schemas.Students;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;

namespace StudentPerfomance.Application.Commands.Student.DeleteStudent;

public sealed class DeleteStudentCommand : ICommand
{
	public ISchemaValidator Validation { get; init; }
	public StudentSchema Student { get; init; }

	public DeleteStudentCommand(StudentSchema student)
	{
		Student = student;
		Validation = new StudentSchemaValidator().WithRecordbookValidation(student);
		Validation.ProcessValidation();
	}
}
