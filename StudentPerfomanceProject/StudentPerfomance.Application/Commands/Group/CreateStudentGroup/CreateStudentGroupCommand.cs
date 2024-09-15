using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.CreateStudentGroup;

internal sealed class CreateStudentGroupCommand : ICommand
{
	public StudentsGroupSchema Group { get; init; }
	public ISchemaValidator Validator { get; init; }
	public IRepositoryExpression<StudentGroup> Expression { get; init; }
	public CreateStudentGroupCommand(StudentsGroupSchema group, IRepositoryExpression<StudentGroup> expression)
	{
		Group = group;
		Validator = new StudentsGroupValidator(group);
		Expression = expression;
		Validator.ProcessValidation();
	}
}
