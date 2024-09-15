using StudentPerfomance.Application.EntitySchemas.Schemas.Teachers;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.TeachersValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Teachers.ChangeTeacherData;

internal sealed class ChangeTeacherDataCommand : ICommand
{
	public TeacherSchema Teacher { get; init; }
	public IRepositoryExpression<Teacher> Existance { get; init; }
	public IRepositoryExpression<Teacher> Dublicate { get; init; }
	public ISchemaValidator Validator { get; init; }

	public ChangeTeacherDataCommand(TeacherSchema schema, IRepositoryExpression<Teacher> existance, IRepositoryExpression<Teacher> dublicate)
	{
		Teacher = schema;
		Existance = existance;
		Dublicate = dublicate;
		Validator = new TeacherSchemaValidator(schema);
		Validator.ProcessValidation();
	}
}
