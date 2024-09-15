using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Application.EntitySchemas.Validators.TeachersValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.DeleteTeacher;

internal sealed class DeleteTeacherCommand : ICommand
{
	public TeacherSchema Teacher { get; init; }
	public IRepositoryExpression<Teacher> Expression { get; init; }
	public TeacherSchemaValidator Validator { get; init; }
	public DeleteTeacherCommand(TeacherSchema schema, IRepositoryExpression<Teacher> expression)
	{
		Teacher = schema;
		Expression = expression;
		Validator = new TeacherSchemaValidator(schema);
		Validator.ProcessValidation();
	}
}
