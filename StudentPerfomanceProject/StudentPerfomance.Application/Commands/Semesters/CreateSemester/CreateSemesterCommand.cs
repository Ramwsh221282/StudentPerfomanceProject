using StudentPerfomance.Application.EntitySchemas.Schemas.Semesters;
using StudentPerfomance.Application.EntitySchemas.Validators.SemesterValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Semesters.CreateSemester;

internal sealed class CreateSemesterCommand : ICommand
{
	public SemesterSchema Semester { get; init; }
	public IRepositoryExpression<Semester> Dublicate { get; init; }
	public IRepositoryExpression<StudentGroup> Existance { get; init; }
	public ISchemaValidator Validator { get; init; }

	public CreateSemesterCommand
	(
		SemesterSchema schema,
		IRepositoryExpression<Semester> dublicate,
		IRepositoryExpression<StudentGroup> existance
	)
	{
		Semester = schema;
		Dublicate = dublicate;
		Existance = existance;
		Validator = new SemesterValidator(schema);
		Validator.ProcessValidation();
	}
}
