using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Application.EntitySchemas.Validators.TeachersValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.CreateTeacher;

internal sealed class CreateTeacherCommand : ICommand
{
	public TeacherSchema Teacher { get; init; }
	public IRepositoryExpression<Teacher> Dublicate { get; init; }
	public IRepositoryExpression<TeachersDepartment> Existance { get; init; }
	public TeacherSchemaValidator Validator { get; init; }
	public CreateTeacherCommand
	(
		TeacherSchema schema,
		IRepositoryExpression<Teacher> dublicate,
		IRepositoryExpression<TeachersDepartment> existance
	)
	{
		Teacher = schema;
		Dublicate = dublicate;
		Existance = existance;
		Validator = new TeacherSchemaValidator(schema);
		Validator.ProcessValidation();
	}
}
