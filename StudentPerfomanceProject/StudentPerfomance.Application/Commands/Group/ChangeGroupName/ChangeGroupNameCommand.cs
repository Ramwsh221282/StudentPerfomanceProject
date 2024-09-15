using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.ChangeGroupName;

internal sealed class ChangeGroupNameCommand : ICommand
{
	public StudentsGroupSchema Group { get; init; }
	public ISchemaValidator Validator { get; init; }
	public IRepositoryExpression<StudentGroup> Existance { get; init; }
	public IRepositoryExpression<StudentGroup> Dublicate { get; init; }
	public ChangeGroupNameCommand
	(
		StudentsGroupSchema schema,
		IRepositoryExpression<StudentGroup> existance,
		IRepositoryExpression<StudentGroup> dublicate
	)
	{
		Group = schema;
		Validator = new StudentsGroupValidator(schema);
		Existance = existance;
		Dublicate = dublicate;
		Validator.ProcessValidation();
	}
}
