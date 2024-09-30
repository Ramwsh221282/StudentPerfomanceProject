using SPerfomance.Application.Shared.Module.Schemas.StudentGroups;
using SPerfomance.Application.Shared.Module.Schemas.StudentGroups.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.StudentGroups;

namespace SPerfomance.Application.StudentGroups.Module.Commands.Update;

internal sealed class StudentGroupUpdateCommand : ICommand
{
	public StudentsGroupSchema NewSchema { get; init; }
	public IRepositoryExpression<StudentGroup> FindInitialGroup { get; init; }
	public IRepositoryExpression<StudentGroup> FindNameDublicate { get; init; }
	public ISchemaValidator Validator { get; init; }
	public StudentGroupUpdateCommand
	(
		StudentsGroupSchema newSchema,
		IRepositoryExpression<StudentGroup> findInitialGroup,
		IRepositoryExpression<StudentGroup> findNameDublicate
	)
	{
		NewSchema = newSchema;
		FindInitialGroup = findInitialGroup;
		FindNameDublicate = findNameDublicate;
		Validator = new StudentGroupSchemaValidator().WithNameValidation(newSchema);
		Validator.ProcessValidation();
	}
}
