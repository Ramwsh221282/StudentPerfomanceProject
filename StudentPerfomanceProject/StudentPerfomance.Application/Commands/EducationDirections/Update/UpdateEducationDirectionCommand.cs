using StudentPerfomance.Application.EntitySchemas.Schemas.EducationDirections;
using StudentPerfomance.Application.EntitySchemas.Validators.EducationDirectionsValidations;
using StudentPerfomance.Application.EntitySchemas.Validators.StudentSchemaValidation;
using StudentPerfomance.Domain.Entities;
using StudentPerfomance.Domain.Interfaces.Repositories;

namespace StudentPerfomance.Application.Commands.EducationDirections.Update;

internal sealed class UpdateEducationDirectionCommand : ICommand
{
	public EducationDirectionSchema OldSchema { get; init; }
	public EducationDirectionSchema NewSchema { get; init; }
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; }
	public IRepositoryExpression<EducationDirection> CheckForCodeDublicate { get; init; }
	public ISchemaValidator SchemaValidator { get; init; }
	public UpdateEducationDirectionCommand
	(
		EducationDirectionSchema oldSchema,
		EducationDirectionSchema newSchema,
		IRepositoryExpression<EducationDirection> findDirection,
		IRepositoryExpression<EducationDirection> checkForDublicate
	)
	{
		OldSchema = oldSchema;
		NewSchema = newSchema;
		FindDirection = findDirection;
		CheckForCodeDublicate = checkForDublicate;
		SchemaValidator = new EducationDirectionValidator()
		.WithNameValidation(newSchema)
		.WithCodeValidator(newSchema)
		.WithTypeValidation(newSchema);
	}
}
