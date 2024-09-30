using SPerfomance.Application.Shared.Module.Schemas.EducationDirections;
using SPerfomance.Application.Shared.Module.Schemas.EducationDirections.Validators;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.CQRS.Commands;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.EntitySchemas;
using SPerfomance.Domain.Module.Shared.Common.Abstractions.Repositories;
using SPerfomance.Domain.Module.Shared.Entities.EducationDirections;

namespace SPerfomance.Application.EducationDirections.Module.Commands.Update;

internal sealed class UpdateEducationDirectionCommand : ICommand
{
	public EducationDirectionSchema NewSchema { get; init; }
	public IRepositoryExpression<EducationDirection> FindDirection { get; init; }
	public IRepositoryExpression<EducationDirection> CheckForCodeDublicate { get; init; }
	public ISchemaValidator SchemaValidator { get; init; }
	public UpdateEducationDirectionCommand
	(
		EducationDirectionSchema newSchema,
		IRepositoryExpression<EducationDirection> findDirection,
		IRepositoryExpression<EducationDirection> checkForDublicate
	)
	{
		NewSchema = newSchema;
		FindDirection = findDirection;
		CheckForCodeDublicate = checkForDublicate;
		SchemaValidator = new EducationDirectionValidator()
		.WithNameValidation(newSchema)
		.WithCodeValidator(newSchema)
		.WithTypeValidation(newSchema);
	}
}
