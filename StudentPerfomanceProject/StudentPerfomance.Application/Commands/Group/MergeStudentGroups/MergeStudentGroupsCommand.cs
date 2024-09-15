using StudentPerfomance.Application.EntitySchemas.Schemas.StudentGroups;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentsGroupSchemaValidation;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.Group.MergeStudentGroups;

internal sealed class MergeStudentGroupsCommand : ICommand
{
	public StudentsGroupSchema GroupA { get; init; }
	public StudentsGroupSchema GroupB { get; init; }
	public ISchemaValidator ValidatorA { get; init; }
	public ISchemaValidator ValidatorB { get; init; }
	public IRepositoryExpression<StudentGroup> ExistanceA { get; init; }
	public IRepositoryExpression<StudentGroup> ExistanceB { get; init; }

	public MergeStudentGroupsCommand
	(
		StudentsGroupSchema groupA,
		StudentsGroupSchema groupB,
		IRepositoryExpression<StudentGroup> existanceA,
		IRepositoryExpression<StudentGroup> existanceB
	)
	{
		GroupA = groupA;
		GroupB = groupB;
		ValidatorA = new StudentsGroupValidator(groupA);
		ValidatorB = new StudentsGroupValidator(groupB);
		ExistanceA = existanceA;
		ExistanceB = existanceB;
		ValidatorA.ProcessValidation();
		ValidatorB.ProcessValidation();
	}
}
